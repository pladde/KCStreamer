using KCStreamer.Models;
using KCStreamer.Services;

namespace KCStreamer;

public partial class MainPage : ContentPage
{
    private readonly BluetoothService _bluetoothService;

    public MainPage()
    {
        InitializeComponent();

        _bluetoothService = new BluetoothService();

        _bluetoothService.OnPowerChanged += OnPowerChanged;

        _bluetoothService.StartScanning();
    }

    private void OnPowerChanged(object sender, SensorData sensorData)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            PowerLabel.Text = $"{sensorData.InstantaneousPowerWatts} W";
            CadenceLabel.Text = $"{sensorData.CumulativeCrankRevolutions} rounds";
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _bluetoothService.OnPowerChanged -= OnPowerChanged;
    }
}