using HAcomms.Models;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;

namespace HAcomms;

public partial class Main : Form {
    private bool _initialized = false;
    private HAcommsModel? _model;
    private INet2HassMqttBridge? _bridge;


    public Main() {
        InitializeComponent();
        InitMqtt();

        // _model.UpdateStatus();
    }

    public void SetMqttStatus(MqttStatus status) {
        this.LblStatusMqtt.Text = Enum.GetName(typeof(MqttStatus), status);
        Console.WriteLine("Update MQTT status: {0}", this.LblStatusMqtt.Text);
    }

    private async void InitMqtt() {
        if (_initialized)
            return;

        SetMqttStatus(MqttStatus.Disconnected);
        var appConfig = new ConfigurationBuilder().AddUserSecrets<Main>().Build();
        _model = new HAcommsModel();
        _bridge = _model.BuildBridge(appConfig);
        
        SetMqttStatus(MqttStatus.Connecting);
        await _bridge.StartAsync();
        _initialized = true;
        SetMqttStatus(MqttStatus.Connected);
        _model.UpdateStatus();
    }

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e) {
        if (!_initialized)
            return;
        _model.InGoogleMeets = !_model.InGoogleMeets;
    }

    private void NotifyIcon_DoubleClick(object sender, EventArgs e) { this.Show(); }

    private async void Main_Closing(object sender, EventArgs e) {
        if (!_initialized)
            return;

        this.NotifyIcon.Visible = false;
        await _bridge.StopAsync();
    }

    protected override void WndProc(ref Message m) {
        if (m.Msg == 0x0112) // WM_SYSCOMMAND
        {
            // Check your window state here
            if (m.WParam == new IntPtr(0XF020)) // Minimize event - SC_MINIMIZE from Winuser.h
            {
                // The window is being minimized
                this.Hide();
                return;
            }
        }

        base.WndProc(ref m);
    }
}