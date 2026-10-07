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

    private void OnPowerChanged(object sender, int watts)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            PowerLabel.Text = $"{watts} W";
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _bluetoothService.OnPowerChanged -= OnPowerChanged;
    }
}