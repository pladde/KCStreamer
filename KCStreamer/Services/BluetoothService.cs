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

        // UUIDs für die Leistungsdaten des KICKR Core (Cycling Power Service)
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

            // Suche nach Gerät mit dem Namen "kickr"
            if (!string.IsNullOrEmpty(deviceName) && deviceName.Contains("kickr", StringComparison.OrdinalIgnoreCase))
            {
                
                _watcher.Stop(); // Ressourcen sparen, weil das Gerät gefunden wurde

                // Verbindung zum Gerät über seine eindeutige Bluetooth-Adresse aufbauen
                _bluetoothDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(args.BluetoothAddress);

                if (_bluetoothDevice != null)
                {
                    await ConnectToGattAsync();
                }
            }
        }

        /// <summary>
        /// Baut eine Verbindung zum GATT-Server des KICKR Core auf und abonniert die Leistungsdaten.
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

                    characteristic.ValueChanged -= Characteristic_ValueChanged; // Falls ein vorheriges Abonnement existiert, wird es entfernt, um doppelte Events zu vermeiden
                    characteristic.ValueChanged += Characteristic_ValueChanged;

                    var status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                        GattClientCharacteristicConfigurationDescriptorValue.Notify);

                    // FIXME: Es wäre gut, hier noch eine Logik einzubauen, welche prüft, ob die Verbindung noch aktiv ist und ggf. neu verbindet, falls sie unterbrochen wird.
                    // Außerdem sollte man überlegen, ob man eine Art "Timeout" einbaut, sodass die Leistung  automatisch auf 0 W zurückfällt wenn längere Zeit keine Events mehr kommen (Tretpause).
                    // Aktuell wird das Event nur gefeuert, wenn der KICKR Core neue Leistungsdaten sendet. Wenn man also aufhört zu treten, kommen keine Events mehr und die Anzeige bleibt auf dem letzten Wert stehen.
                    // FIXME: Zudem gibt es immer wieder "Freezes" wenn man nicht tritt. Ich vermute dass das mit dem BLE-Stack zusammenhängt, der die Verbindung verliert und nicht
                    // automatisch wiederherstellt. Hier müsste man ggf. eine Reconnect-Logik einbauen.

                    // DEBUG für die Aboabfrage
                    if (status == GattCommunicationStatus.Success)
                    {
                        System.Diagnostics.Debug.WriteLine("[BLE] GATT erfolgreich abonniert!");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[BLE] Fehler beim Abonnieren: {status}");
                    }
                }
            }
        }

        /// <summary>
        /// Sobal der KICKR Core neue Leistungsdaten sendet, wird diese Methode aufgerufen.
        /// </summary>
        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            using var reader = DataReader.FromBuffer(args.CharacteristicValue);
            byte[] data = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(data);

            System.Diagnostics.Debug.WriteLine($"[BLE] Daten empfangen. Länge: {data.Length}");

            if (data.Length >= 4)
            {
                short watts = BitConverter.ToInt16(data, 2);

                OnPowerChanged?.Invoke(this, watts); // Wattzahen werden als Event weitergegeben, sodass andere Teile der Anwendung darauf reagieren können
            }
        }

    }
}
