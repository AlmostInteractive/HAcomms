using System.Text.Json;
using HAcomms.Tools;
using HAcomms.Models;
using Microsoft.Extensions.Configuration;
using NoeticTools.Net2HassMqtt;
using BrowserTabs = HAcomms.Tools.BrowserTabs;
using Timer = System.Windows.Forms.Timer;

namespace HAcomms.Forms;

public partial class Main : Form {
    private bool _settingsLoaded = false;
    private HAcommsModel? _model;
    private INet2HassMqttBridge? _bridge;
    private MqttStatus _mqttStatus = MqttStatus.Disconnected;
    private readonly List<WatchedEntity> _watchedEntities = [];
    private readonly Timer _scanTimer = new();
    private bool _inScan = false;
    private bool _refreshingTabs = false;
    private IDictionary<IntPtr, string> _openWindows;
    private Dictionary<IntPtr, string> _firefoxWindows;
    private Dictionary<IntPtr, string> _chromeWindows;


    public Main() {
        InitializeComponent();
        SetMqttStatus(MqttStatus.Disconnected);
        LoadSettings();
    }

    public async void ReloadSettings() {
        LoadSettings();
        if (!_settingsLoaded) {
            return;
        }

        if (_mqttStatus != MqttStatus.Disconnected) {
            await _bridge!.StopAsync();
            SetMqttStatus(MqttStatus.Disconnected);
            _scanTimer.Stop();
        }

        InitMqtt();
    }

    private void ShowSettingsDialog() {
        using var form = new Settings();
        form.ShowDialog(this);
    }

    private bool MqttIsConnected => _mqttStatus == MqttStatus.Connected;

    private void SetMqttStatus(MqttStatus status) {
        _mqttStatus = status;
        this.LblStatusMqtt.Text = Enum.GetName(typeof(MqttStatus), status);
    }

    private void LoadSettings() {
        _settingsLoaded = false;

        string address = Properties.Settings.Default.MqttBroker_Address;
        string username = Properties.Settings.Default.MqttBroker_Username;
        string password = Properties.Settings.Default.MqttBroker_Password;

        if (address == "" || username == "" || password == "") {
            return;
        }

        _watchedEntities.Clear();
        var entities = JsonSerializer.Deserialize<WatchedEntity[]>(Properties.Settings.Default.WatchedEntities);
        if (entities != null) {
            _watchedEntities.AddRange(entities);
        }

        this.ListBoxEntries.Items.Clear();
        foreach (var we in _watchedEntities) {
            AddWatchedEntryListItem(we);
        }

        _settingsLoaded = true;
    }

    private void SaveWatchedEntities() {
        string json = JsonSerializer.Serialize(_watchedEntities);
        Properties.Settings.Default.WatchedEntities = json;
        Properties.Settings.Default.Save();
    }

    private async void InitMqtt() {
        if (!_settingsLoaded) {
            return;
        }

        var appConfig = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>() {
                ["MqttBroker:Address"] = Properties.Settings.Default.MqttBroker_Address,
                ["MqttBroker:Username"] = Properties.Settings.Default.MqttBroker_Username,
                ["MqttBroker:Password"] = Properties.Settings.Default.MqttBroker_Password,
            })
            .Build();

        _model = new HAcommsModel();
        _bridge = _model.BuildBridge(appConfig);

        SetMqttStatus(MqttStatus.Connecting);
        bool connected = await _bridge.StartAsync();
        if (!connected) {
            SetMqttStatus(MqttStatus.Error);
        } else {
            SetMqttStatus(MqttStatus.Connected);

            _scanTimer.Interval = (int)(1.5 * 1000); // in ms
            _scanTimer.Tick += PerformScan;
            _scanTimer.Start();
        }
    }

    private void RefreshWindows() {
        this.BtnRefreshWindows.Enabled = false;
        var windows = WindowsTools.GetOpenWindows();
        this.ListBoxWindows.Items.Clear();
        foreach (var kvp in windows) {
            string title = kvp.Value;
            this.ListBoxWindows.Items.Add(title);
        }
        this.BtnRefreshWindows.Enabled = true;
    }

    private async Task RefreshTabs() {
        if (_refreshingTabs) {
            return;
        }

        _refreshingTabs = true;
        this.BtnRefreshTabs.Enabled = false;
        
        var windows = WindowsTools.GetOpenWindows();
        var firefoxes = windows.Where(kvp => kvp.Value.EndsWith("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.EndsWith("Google Chrome")).ToDictionary();

        this.ListBoxTabs.Items.Clear();

        if (firefoxes.Keys.Count > 0) {
            foreach (var tab in BrowserTabs.GetAllTabTitles<FirefoxBrowser>(firefoxes.Keys)) {
                this.ListBoxTabs.Items.Add(tab);
            }
        }

        if (chromes.Keys.Count > 0) {
            foreach (var tab in BrowserTabs.GetAllTabTitles<ChromeBrowser>(chromes.Keys)) {
                this.ListBoxTabs.Items.Add(tab);
            }
        }

        this.BtnRefreshTabs.Enabled = true;
        _refreshingTabs = false;
    }

    private void AddWatchedEntryListItem(WatchedEntity we) {
        string x = $"[{(we.IsTab ? "T" : "A")}] {(we.IsRegex ? "/" : "\"")}{we.Entry}{(we.IsRegex ? "/" : "\"")}";
        this.ListBoxEntries.Items.Add(x);
    }

    private void PerformScan(object? sender, EventArgs e) {
        if (!MqttIsConnected || _inScan) {
            return;
        }

        _inScan = true;
        
        _openWindows = WindowsTools.GetOpenWindows();
        _firefoxWindows = _openWindows.Where(kvp => kvp.Value.EndsWith("Mozilla Firefox")).ToDictionary();
        _chromeWindows = _openWindows.Where(kvp => kvp.Value.EndsWith("Google Chrome")).ToDictionary();
        
        ScanForWatchedEntities();
        ScanForMeetings();
        
        _inScan = false;
    }

    private void ScanForWatchedEntities() {
        // search applications
        if (_watchedEntities.Where(we => !we.IsTab).Any(we => WindowsTools.AnyTitleMatches(we, _openWindows))) {
            _model!.WatchedEntriesPresent = true;
            return;
        }

        var weTabs = _watchedEntities.Where(we => we.IsTab).ToList();

        BrowserTabs.ResetCache();

        if (weTabs.Any(we => _chromeWindows.Any(kvp => BrowserTabs.MatchesWatchedEntity<ChromeBrowser>(kvp.Key, we)))) {
            _model!.WatchedEntriesPresent = true;
            return;
        }
        
        if (weTabs.Any(we => _firefoxWindows.Any(kvp => BrowserTabs.MatchesWatchedEntity<FirefoxBrowser>(kvp.Key, we)))) {
            _model!.WatchedEntriesPresent = true;
            return;
        }

        _model!.WatchedEntriesPresent = false;
    }

    private void ScanForMeetings() {
        if (!BrowserTabs.CheckForMeetings(_chromeWindows.Values.ToList())) {
            BrowserTabs.CheckForMeetings(_firefoxWindows.Values.ToList());
        }
    }

    private void Main_Shown(object sender, EventArgs e) {
        RefreshWindows();
        Task.Run(RefreshTabs);

        if (!_settingsLoaded) {
            ShowSettingsDialog();
        } else {
            InitMqtt();
        }
    }
    
    private void NotifyIcon_DoubleClick(object sender, EventArgs e) { this.Show(); }

    private void MenuItemSettings_Click(object sender, EventArgs e) { ShowSettingsDialog(); }

    private void MenuItemExit_Click(object sender, EventArgs e) { this.Close(); }

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

    private async void BtnRefreshTabs_Click(object sender, EventArgs e) { await RefreshTabs(); }

    private async void Main_Closing(object sender, EventArgs e) {
        this.NotifyIcon.Visible = false;
        this.NotifyIcon.Icon?.Dispose();
        this.NotifyIcon.Dispose();
        _scanTimer.Stop();

        if (!MqttIsConnected)
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