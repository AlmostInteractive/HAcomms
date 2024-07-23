using System.Text.Json;
using HAcomms.Models;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using HAcomms.Tools;
using Timer = System.Windows.Forms.Timer;

namespace HAcomms;

public partial class Main : Form {
    private bool _initialized = false;
    private HAcommsModel? _model;
    private INet2HassMqttBridge? _bridge;
    private readonly List<WatchedEntity> _watchedEntities = [];
    private Timer _scanTimer = new();
    private bool _inScan = false;


    public Main() {
        InitializeComponent();
        LoadWatchedEntities();

        _scanTimer.Interval = (int)(1.5 * 1000); // in ms
        _scanTimer.Tick += PerformScan;
        _scanTimer.Start();
    }

    public void SetMqttStatus(MqttStatus status) {
        this.LblStatusMqtt.Text = Enum.GetName(typeof(MqttStatus), status);
        Console.WriteLine("Update MQTT status: {0}", this.LblStatusMqtt.Text);
    }

    private void LoadWatchedEntities() {
        _watchedEntities.Clear();
        var entities = JsonSerializer.Deserialize<WatchedEntity[]>(Properties.Settings.Default.WatchedEntities);
        if (entities != null) {
            _watchedEntities.AddRange(entities);
        }

        this.ListBoxEntries.Items.Clear();
        foreach (var we in _watchedEntities) {
            AddWatchedEntryListItem(we);
        }
    }

    private void SaveWatchedEntities() {
        string json = JsonSerializer.Serialize(_watchedEntities);
        Properties.Settings.Default.WatchedEntities = json;
        Properties.Settings.Default.Save();
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
        var firefoxes = windows.Where(kvp => kvp.Value.EndsWith("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.EndsWith("Google Chrome")).ToDictionary();

        this.ListBoxTabs.Items.Clear();
        foreach (var tab in Chrome.GetAllTabTitles(chromes.Keys)) {
            this.ListBoxTabs.Items.Add(tab);
        }
    }

    private void AddWatchedEntryListItem(WatchedEntity we) {
        string x = $"[{(we.IsTab ? "T" : "A")}] {(we.IsRegex ? "/" : "\"")}{we.Entry}{(we.IsRegex ? "/" : "\"")}";
        this.ListBoxEntries.Items.Add(x);
    }

    private void PerformScan(object sender, EventArgs e) {
        Console.WriteLine("Perform Scan");
        if (!_initialized || _inScan) {
            return;
        }

        _inScan = true;

        // search applications
        var windows = WindowsTools.GetOpenWindows();

        if (_watchedEntities.Where(we => !we.IsTab).Any(we => WindowsTools.AnyTitleMatches(we, windows))) {
            _model.WatchedEntriesPresent = true;
            _inScan = false;
            return;
        }

        var firefoxes = windows.Where(kvp => kvp.Value.EndsWith("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.EndsWith("Google Chrome")).ToDictionary();
        var tabs = _watchedEntities.Where(we => we.IsTab);
        
        // reset caches
        Chrome.ResetCache();

        if (tabs.Any(we => chromes.Any(kvp => Chrome.MatchesWatchedEntity(kvp.Key, we)))) {
            _model.WatchedEntriesPresent = true;
            _inScan = false;
            return;
        }

        _model.WatchedEntriesPresent = false;
        _inScan = false;
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

    private void ListBox_SelectedIndexChanged(object sender, EventArgs e) {
        var listBox = sender as ListBox;
        if (listBox?.SelectedItem == null) {
            return;
        }

        string? curItem = listBox.SelectedItem.ToString();

        if (listBox == this.ListBoxWindows) {
            this.ListBoxTabs.SelectedItem = null;
            this.ListBoxEntries.SelectedItem = null;
            this.RbApplication.Checked = true;
        } else if (listBox == this.ListBoxTabs) {
            this.ListBoxWindows.SelectedItem = null;
            this.ListBoxEntries.SelectedItem = null;
            this.RbTab.Checked = true;
        } else {
            this.ListBoxWindows.SelectedItem = null;
            this.ListBoxTabs.SelectedItem = null;

            var we = _watchedEntities[listBox.SelectedIndex];
            if (we.IsTab) {
                this.RbTab.Checked = true;
            } else {
                this.RbApplication.Checked = true;
            }

            if (we.IsRegex) {
                this.RbRegex.Checked = true;
            } else {
                this.RbLiteral.Checked = true;
            }

            curItem = curItem?.Remove(curItem.Length - 1).Substring(5);
        }

        this.TextBoxEntryEditor.Text = (curItem ?? "");
    }

    private void BtnAdd_Click(object sender, EventArgs e) {
        string entry = this.TextBoxEntryEditor.Text;
        if (entry.Trim().Length == 0) {
            return;
        }

        var we = new WatchedEntity() {
            IsTab = this.RbTab.Checked,
            IsRegex = this.RbRegex.Checked,
            Entry = entry
        };
        _watchedEntities.Add(we);
        AddWatchedEntryListItem(we);
        SaveWatchedEntities();
    }

    private void BtnRemove_Click(object sender, EventArgs e) {
        int idx = this.ListBoxEntries.SelectedIndex;
        if (idx == -1) {
            return;
        }

        _watchedEntities.RemoveAt(idx);
        this.ListBoxEntries.Items.RemoveAt(idx);
        SaveWatchedEntities();
    }

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