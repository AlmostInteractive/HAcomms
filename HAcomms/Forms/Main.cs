using System.Text.Json;
using HAcomms.Tools;
using HAcomms.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NoeticTools.Net2HassMqtt;
using BrowserTabs = HAcomms.Tools.BrowserTabs;
using Timer = System.Windows.Forms.Timer;

namespace HAcomms.Forms;

public partial class Main : Form {
    private const int WM_SYSCOMMAND = 0x112;
    private const IntPtr SC_MINIMIZE = 0XF020;
    
    private bool _startMinimized;
    private bool _settingsLoaded;
    private HAcommsModel? _model;
    private INet2HassMqttBridge? _bridge;
    private MqttStatus _mqttStatus = MqttStatus.Disconnected;
    private readonly List<WatchedEntity> _watchedEntities = [];
    private readonly Timer _scanTimer = new();
    private bool _inScan;
    private bool _refreshingTabs;
    private IDictionary<IntPtr, string>? _openWindows;
    private Dictionary<IntPtr, string>? _firefoxWindows;
    private Dictionary<IntPtr, string>? _chromeWindows;
    private readonly GlobalKeyboardHook _globalKeyboardHook;
    private bool _recordKeys;
    private KeyCombination? _lastKeyCombo;
    private readonly Audio _audio;

    public Main(bool minimized) {
        _startMinimized = minimized;
        _scanTimer.Interval = (int)(1.5 * 1000); // in ms
        _scanTimer.Tick += PerformScan;

        _globalKeyboardHook = new GlobalKeyboardHook();
        _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        _globalKeyboardHook.KeyboardComboPressed += OnComboPressed;

        _audio = new Audio();
        _audio.HookVolumeNotification(OnVolumeChanged);

        InitializeComponent();
        SetMqttStatus(MqttStatus.Disconnected);
        LoadSettings();
    }

    private void ShowMainForm() {
        this.Show();
        this.WindowState = FormWindowState.Normal;
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
        // because this can happen before form has been created
        if (this.LblStatusMqtt.IsHandleCreated) {
            var del = () => { this.LblStatusMqtt.Text = Enum.GetName(typeof(MqttStatus), status); };
            this.LblStatusMqtt.Invoke(del);
        }
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

        var combos = JsonSerializer.Deserialize<KeyCombination[]>(Properties.Settings.Default.KeyCombos);
        if (combos != null) {
            _globalKeyboardHook.SetKeyCombos(combos);
        }

        this.ListBoxCombos.Items.Clear();
        foreach (var combo in _globalKeyboardHook.RegisteredKeyCombos) {
            AddComboListItem(combo);
        }

        _settingsLoaded = true;
    }

    private void SaveWatchedEntities() {
        string json = JsonSerializer.Serialize(_watchedEntities);
        Properties.Settings.Default.WatchedEntities = json;
        Properties.Settings.Default.Save();
    }

    private void SaveKeyCombos() {
        var combos = _globalKeyboardHook.RegisteredKeyCombos;
        string json = JsonSerializer.Serialize(combos);
        Properties.Settings.Default.KeyCombos = json;
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
            StartScans();
        }
    }

    private void RefreshWindows() {
        var del = () => { this.BtnRefreshWindows.Enabled = false; };
        this.BtnRefreshWindows.Invoke(del);

        var windows = WindowsTools.GetOpenWindows();
        del = () => { this.ListBoxWindows.Items.Clear(); };
        this.ListBoxWindows.Invoke(del);

        var addDelegate = (string tab) => this.ListBoxWindows.Items.Add(tab);
        foreach (var kvp in windows) {
            string title = kvp.Value;
            this.ListBoxWindows.Invoke(addDelegate, title);
        }

        del = () => { this.BtnRefreshWindows.Enabled = true; };
        this.BtnRefreshWindows.Invoke(del);
    }

    private void RefreshTabs() {
        if (_refreshingTabs) {
            return;
        }

        _refreshingTabs = true;

        var del = () => { this.BtnRefreshTabs.Enabled = false; };
        this.BtnRefreshTabs.Invoke(del);

        var windows = WindowsTools.GetOpenWindows();
        var firefoxes = windows.Where(kvp => kvp.Value.EndsWith("Mozilla Firefox")).ToDictionary();
        var chromes = windows.Where(kvp => kvp.Value.EndsWith("Google Chrome")).ToDictionary();

        del = () => { this.ListBoxTabs.Items.Clear(); };
        this.ListBoxTabs.Invoke(del);

        var addDelegate = (string tab) => this.ListBoxTabs.Items.Add(tab);
        if (firefoxes.Keys.Count > 0) {
            foreach (var tab in BrowserTabs.GetAllTabTitles<FirefoxBrowser>(firefoxes.Keys)) {
                this.ListBoxTabs.Invoke(addDelegate, tab);
            }
        }

        if (chromes.Keys.Count > 0) {
            foreach (var tab in BrowserTabs.GetAllTabTitles<ChromeBrowser>(chromes.Keys)) {
                this.ListBoxTabs.Invoke(addDelegate, tab);
            }
        }

        del = () => { this.BtnRefreshTabs.Enabled = true; };
        this.BtnRefreshTabs.Invoke(del);

        _refreshingTabs = false;
    }

    private void AddWatchedEntryListItem(WatchedEntity we) {
        string x = $"[{(we.IsTab ? "T" : "A")}] {(we.IsRegex ? "/" : "\"")}{we.Entry}{(we.IsRegex ? "/" : "\"")}";
        this.ListBoxEntries.Items.Add(x);
    }

    private void AddComboListItem(KeyCombination combo) { AddComboListItem(combo.Id, combo); }

    private void AddComboListItem(string id, KeyCombination combo) { this.ListBoxCombos.Items.Add($"{id}: {combo}"); }

    private void StartScans() {
        _scanTimer.Start();
        _model!.Mute = _audio.IsMuted;
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
        ScanForHwDevices();

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

        if (weTabs.Any(we => _chromeWindows!.Any(kvp => BrowserTabs.MatchesWatchedEntity<ChromeBrowser>(kvp.Key, we)))) {
            _model!.WatchedEntriesPresent = true;
            return;
        }

        if (weTabs.Any(we => _firefoxWindows!.Any(kvp => BrowserTabs.MatchesWatchedEntity<FirefoxBrowser>(kvp.Key, we)))) {
            _model!.WatchedEntriesPresent = true;
            return;
        }

        _model!.WatchedEntriesPresent = false;
    }

    private void ScanForHwDevices() {
        _model!.WebcamInUse = IsWebCamInUse();
        _model!.MicrophoneInUse = IsMicrophoneInUse();
    }

    private static bool IsWebCamInUse() {
        using var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\NonPackaged");
        return IsHardwareInUse(key);
    }

    private static bool IsMicrophoneInUse() {
        using var key = Registry.CurrentUser.OpenSubKey(
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone\NonPackaged");
        return IsHardwareInUse(key);
    }

    private static bool IsHardwareInUse(RegistryKey? key) {
        if (key == null) {
            return false;
        }

        long latestStartTime = 0;
        bool retVal = false;

        foreach (string subKeyName in key.GetSubKeyNames()) {
            using var subKey = key.OpenSubKey(subKeyName);
            if (subKey == null || !subKey.GetValueNames().Contains("LastUsedTimeStop")) {
                continue;
            }

            // only record the state of the most recent start time
            long startTime = subKey.GetValue("LastUsedTimeStart") is long
                ? (long)subKey.GetValue("LastUsedTimeStart")!
                : -1;

            if (startTime <= latestStartTime) {
                continue;
            }

            latestStartTime = startTime;

            long endTime = subKey.GetValue("LastUsedTimeStop") is long
                ? (long)subKey.GetValue("LastUsedTimeStop")!
                : -1;

            retVal = (endTime <= 0);
        }

        return retVal;
    }

    private void OnKeyPressed(object? sender, GlobalKeyboardHookEventArgs e) {
        if (!_recordKeys) {
            return;
        }

        // to consume the keystroke
        e.Handled = true;

        if (e.KeyboardState is not (GlobalKeyboardHook.KeyboardState.SysKeyDown or GlobalKeyboardHook.KeyboardState.KeyDown)) {
            return;
        }

        // check to see if we should skip processing this
        if (_lastKeyCombo != null) {
            var key = e.KeyboardData.Key;
            if (_lastKeyCombo.Equals(_globalKeyboardHook.CurrentKeyCombo)) {
                return;
            }

            if (GlobalKeyboardHook.IsKeyAlt(key) && _lastKeyCombo.WithAlt) {
                return;
            }

            if (GlobalKeyboardHook.IsKeyCtrl(key) && _lastKeyCombo.WithCtrl) {
                return;
            }
        }

        Console.WriteLine($"{e.KeyboardData.Key}");
        _lastKeyCombo = _globalKeyboardHook.CurrentKeyCombo;
        this.TextBoxKeyCombo.Text = _lastKeyCombo.ToString();
    }

    private void OnComboPressed(object? sender, GlobalKeyboardKeyCombinationEventArgs e) {
        if (!MqttIsConnected) {
            return;
        }

        _model!.FireKeyboardComboEvent(("combo_id", e.ComboId));
    }

    private void OnVolumeChanged(AudioVolumeNotificationData data) {
        _model!.Mute = _audio.IsMuted;
    }

    private void Main_Shown(object sender, EventArgs e) {
        if (_startMinimized) {
            _startMinimized = false;
            this.Hide();
        }

        RefreshWindows();
        var t = Task.Run(RefreshTabs);

        if (!_settingsLoaded) {
            ShowSettingsDialog();
        } else {
            t.ContinueWith((_) => {
                var del = InitMqtt;
                this.Invoke(del);
            });
        }
    }

    private void NotifyIcon_DoubleClick(object sender, EventArgs e) { ShowMainForm(); }

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

    private void BtnAddEntry_Click(object sender, EventArgs e) {
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

    private void BtnRemoveEntry_Click(object sender, EventArgs e) {
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

    private void TextBoxKeyCombo_GotFocus(object sender, EventArgs e) { _recordKeys = true; }

    private void TextBoxKeyCombo_LostFocus(object sender, EventArgs e) { _recordKeys = false; }

    private void BtnAddCombo_Click(object sender, EventArgs e) {
        if (_lastKeyCombo == null || _lastKeyCombo.Count == 0) {
            return;
        }

        string id = this.TextBoxComboId.Text.Trim();
        if (string.IsNullOrEmpty(id)) {
            this.TextBoxComboId.Focus();
            return;
        }

        this.TextBoxComboId.Clear();

        if (!_globalKeyboardHook.AddKeyCombo(id, _lastKeyCombo)) {
            this.TextBoxComboId.Focus();
            return;
        }

        this.TextBoxKeyCombo.Clear();

        AddComboListItem(id, _lastKeyCombo);
        _lastKeyCombo = null;

        SaveKeyCombos();
    }

    private void BtnRemoveCombo_Click(object sender, EventArgs e) {
        int idx = this.ListBoxCombos.SelectedIndex;
        if (idx == -1) {
            return;
        }

        string id = this.ListBoxCombos.SelectedItem!.ToString()!.Split(":")[0];

        _globalKeyboardHook.RemoveKeyCombo(id);
        this.ListBoxCombos.Items.RemoveAt(idx);
        SaveKeyCombos();
    }

    private async void Main_Closing(object sender, EventArgs e) {
        this.NotifyIcon.Visible = false;
        this.NotifyIcon.Icon?.Dispose();
        this.NotifyIcon.Dispose();
        _scanTimer.Stop();

        if (!MqttIsConnected)
            await _bridge!.StopAsync();
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e) { ShowMainForm(); }

    private void exitToolStripMenuItem1_Click(object sender, EventArgs e) { this.Close(); }

    protected override void WndProc(ref Message m) {
        if (m.Msg == WM_SYSCOMMAND && m.WParam == SC_MINIMIZE) {
            // The window is being minimized
            this.Hide();
            return;
        }

        base.WndProc(ref m);
    }
}