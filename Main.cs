using HAcomms.Models;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using HAcomms.Tools;

namespace HAcomms;

public partial class Main : Form {
    private bool _initialized = false;
    private HAcommsModel? _model;
    private INet2HassMqttBridge? _bridge;


    public Main() { InitializeComponent(); }

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
        SetMqttStatus(MqttStatus.Connected);

        OnInitialization();
    }

    private void OnInitialization() {
        if (_initialized) {
            return;
        }

        _initialized = true;
    }

    private void RefreshWindows() {
        var windows = WindowsTools.GetOpenWindows();
        this.ListBoxWindows.Items.Clear();
        foreach (var kvp in windows) {
            string title = kvp.Value;
            this.ListBoxWindows.Items.Add(title);
        }
    }

    private void RefreshTabs() {
        var windows = WindowsTools.GetOpenWindows();
        var firefoxes = windows.Where(kvp => kvp.Value.Contains("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.Contains("Google Chrome")).ToDictionary();

        this.ListBoxTabs.Items.Clear();
        foreach (var tab in Chrome.GetAllTabTitles(chromes.Keys)) {
            this.ListBoxTabs.Items.Add(tab);
        }
    }

    private void Main_Shown(object sender, EventArgs e) {
        InitMqtt();
        RefreshWindows();
        RefreshTabs();
    }

    private void NotifyIcon_MouseClick(object sender, MouseEventArgs e) {
        if (!_initialized) {
            return;
        }

        _model!.WatchedEntriesPresent = !_model.WatchedEntriesPresent;
    }

    private void NotifyIcon_DoubleClick(object sender, EventArgs e) { this.Show(); }

    private void ListBoxWindowsTabs_SelectedIndexChanged(object sender, EventArgs e) {
        var listBox = sender as ListBox;
        if (listBox?.SelectedItem == null) {
            return;
        }

        string? curItem = listBox.SelectedItem.ToString();

        if (listBox == this.ListBoxWindows) {
            this.ListBoxTabs.SelectedItem = null;
            this.RbApplication.Checked = true;
        } else {
            this.ListBoxWindows.SelectedItem = null;
            this.RbTab.Checked = true;
        }

        this.TextBoxEntryEditor.Text = (curItem ?? "");
    }

    private void BtnAdd_Click(object sender, EventArgs e) { throw new System.NotImplementedException(); }

    private void BtnRefreshWindows_Click(object sender, EventArgs e) { RefreshWindows(); }

    private void BtnRefreshTabs_Click(object sender, EventArgs e) { RefreshTabs(); }

    private async void Main_Closing(object sender, EventArgs e) {
        this.NotifyIcon.Visible = false;
        this.NotifyIcon.Icon?.Dispose();
        this.NotifyIcon.Dispose();

        if (!_initialized)
            return;

        await _bridge!.StopAsync();
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