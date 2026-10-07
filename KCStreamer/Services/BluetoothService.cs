using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace KCStreamer.Services
{

    /// <summary>
    /// Diese Klasse ist für die Verwaltung von Bluetooth-Verbindungen und Datenübertragung zuständig.
    /// 
    /// <para><b>Verfügbare Methoden:</b></para>
    /// <list type="bullet">
    ///   <item>
    ///     <term><c>public StartScanning()</c></term>
    ///     <description>Diese Methode startet den Watcher für BLE-Advertises.</description>
    ///   </item>
    /// </list>
    /// 
    /// </summary>
    internal class BluetoothService
    {
        private BluetoothLEAdvertisementWatcher _watcher;
        private BluetoothLEDevice _bluetoothDevice;

        // UUIDs für die LEistungsdaten des KICKR Core (Cycling Power Service)
        private static readonly Guid CyclingPowerServiceUuid = new Guid("00001818-0000-1000-8000-00805f9b34fb");
        private static readonly Guid CyclingPowerMeasurementCharacteristicUuid = new Guid("00002a63-0000-1000-8000-00805f9b34fb");

        public event EventHandler<int> OnPowerChanged;

        /// <summary>
        /// Scannt nach Bluetooth-Geräten in der Nähe und sucht gezielt nach dem KICKR Core.
        /// </summary>
        public void StartScanning()
        {
            _watcher = new BluetoothLEAdvertisementWatcher
            {
                ScanningMode = BluetoothLEScanningMode.Active
            };

            _watcher.Received += OnAdvertisementReceived;
            _watcher.Start();
        }

        private async void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            string deviceName = args.Advertisement.LocalName;

            // Wir suchen gezielt nach einem Gerät, dessen Name "KICKR" enthält
            if (!string.IsNullOrEmpty(deviceName) && deviceName.Contains("KICKR", StringComparison.OrdinalIgnoreCase))
            {
                // Sobald gefunden, stoppen wir den Scanner, um Ressourcen zu sparen
                _watcher.Stop();

                // Verbindung zum Gerät über seine eindeutige Bluetooth-Adresse aufbauen
                _bluetoothDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);

                if (_bluetoothDevice != null)
                {
                    await ConnectToGattAsync();
                }
            }
        }

        /// <summary>
        /// Baut eine VErbindung zum GATT-Server des KICKR Core auf und abonniert die Leistungsdaten.
        /// </summary>
        private async Task ConnectToGattAsync()
        {
            var result = await _bluetoothDevice.GetGattServicesForUuidAsync(CyclingPowerServiceUuid);

            if (result.Status == GattCommunicationStatus.Success && result.Services.Count > 0)
            {
                var service = result.Services[0];

                var charResult = await service.GetCharacteristicsForUuidAsync(CyclingPowerMeasurementCharacteristicUuid);

                if (charResult.Status == GattCommunicationStatus.Success && charResult.Characteristics.Count > 0)
                {
                    var characteristic = charResult.Characteristics[0];

                    var status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                        GattClientCharacteristicConfigurationDescriptorValue.Notify);

                    if (status == GattCommunicationStatus.Success)
                    {
                        characteristic.ValueChanged += Characteristic_ValueChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Sobal d der KICKR Core neue Leistungsdaten sendet, wird diese Methode aufgerufen.
        /// </summary>
        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            using var reader = DataReader.FromBuffer(args.CharacteristicValue);
            byte[] data = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(data);

            if (data.Length >= 4)
            {
                short watts = BitConverter.ToInt16(data, 2);

                OnPowerChanged?.Invoke(this, watts); // Wattzahen werden als Event weitergegeben, sodass andere Teile der Anwendung darauf reagieren können
            }
        }
    }
}
