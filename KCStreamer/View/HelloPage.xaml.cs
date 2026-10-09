using KCStreamer.Models;
using KCStreamer.Services;

namespace KCStreamer.View;

public partial class HelloPage : ContentPage
{
    private readonly BluetoothService _bluetoothService;

    public HelloPage()
	{
		InitializeComponent();

        _bluetoothService = new BluetoothService();

        _bluetoothService.ConnectedDeviceName += OnConnection;

        _bluetoothService.StartScanning();
    }

    private void OnConnection(object sender, string connectedDeviceName)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ConnectedDeviceName.Text = $"Mit {connectedDeviceName} verbunden";
        });
    }
}