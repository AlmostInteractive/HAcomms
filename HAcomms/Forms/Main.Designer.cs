namespace HAcomms.Forms;

partial class Main {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }


    #region Windows Form Designer generated code
    
    private System.Windows.Forms.NotifyIcon NotifyIcon;

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        NotifyIcon = new NotifyIcon(components);
        LblStatusMqtt = new Label();
        LblMqtt = new Label();
        LblOpenWindows = new Label();
        LblOpenTabs = new Label();
        LblEntries = new Label();
        ListBoxWindows = new ListBox();
        ListBoxTabs = new ListBox();
        ListBoxEntries = new ListBox();
        BtnRefreshWindows = new Button();
        BtnRefreshTabs = new Button();
        BtnAdd = new Button();
        BtnRemove = new Button();
        TextBoxEntryEditor = new TextBox();
        GroupLiteralRegex = new GroupBox();
        RbLiteral = new RadioButton();
        RbRegex = new RadioButton();
        GroupAppTab = new GroupBox();
        RbApplication = new RadioButton();
        RbTab = new RadioButton();
        menuMain = new MenuStrip();
        toolStripMenuItemFile = new ToolStripMenuItem();
        settingsToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        GroupLiteralRegex.SuspendLayout();
        GroupAppTab.SuspendLayout();
        menuMain.SuspendLayout();
        SuspendLayout();
        // 
        // NotifyIcon
        // 
        NotifyIcon.BalloonTipTitle = "HAcomms";
        NotifyIcon.Icon = Properties.Resources.NotifyIcon_Icon;
        NotifyIcon.Text = "HAcomms";
        NotifyIcon.Visible = true;
        NotifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        // 
        // LblStatusMqtt
        // 
        LblStatusMqtt.AccessibleDescription = "MQTT status.";
        LblStatusMqtt.AccessibleName = "MQTT status";
        LblStatusMqtt.AccessibleRole = AccessibleRole.StaticText;
        LblStatusMqtt.AutoSize = true;
        LblStatusMqtt.Location = new Point(50, 30);
        LblStatusMqtt.Name = "LblStatusMqtt";
        LblStatusMqtt.Size = new Size(12, 15);
        LblStatusMqtt.TabIndex = 2;
        LblStatusMqtt.Text = "-";
        // 
        // LblMqtt
        // 
        LblMqtt.AccessibleDescription = "Description of the MQTT client.";
        LblMqtt.AccessibleName = "MQTT  description";
        LblMqtt.AccessibleRole = AccessibleRole.StaticText;
        LblMqtt.Location = new Point(10, 30);
        LblMqtt.Name = "LblMqtt";
        LblMqtt.Size = new Size(100, 23);
        LblMqtt.TabIndex = 3;
        LblMqtt.Text = "MQTT:";
        // 
        // LblOpenWindows
        // 
        LblOpenWindows.AccessibleDescription = "Running applications.";
        LblOpenWindows.AccessibleName = "Running applicaitons";
        LblOpenWindows.AccessibleRole = AccessibleRole.StaticText;
        LblOpenWindows.AutoSize = true;
        LblOpenWindows.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        LblOpenWindows.Location = new Point(10, 55);
        LblOpenWindows.Name = "LblOpenWindows";
        LblOpenWindows.Size = new Size(117, 24);
        LblOpenWindows.TabIndex = 4;
        LblOpenWindows.Text = "Applications:";
        // 
        // LblOpenTabs
        // 
        LblOpenTabs.AccessibleDescription = "Browser tabs.";
        LblOpenTabs.AccessibleName = "Browser tabs";
        LblOpenTabs.AccessibleRole = AccessibleRole.StaticText;
        LblOpenTabs.AutoSize = true;
        LblOpenTabs.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        LblOpenTabs.Location = new Point(276, 55);
        LblOpenTabs.Name = "LblOpenTabs";
        LblOpenTabs.Size = new Size(57, 24);
        LblOpenTabs.TabIndex = 5;
        LblOpenTabs.Text = "Tabs:";
        // 
        // LblEntries
        // 
        LblEntries.AccessibleDescription = "Watched apps and tabs.";
        LblEntries.AccessibleName = "Watched apps and tabs";
        LblEntries.AccessibleRole = AccessibleRole.StaticText;
        LblEntries.AutoSize = true;
        LblEntries.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        LblEntries.Location = new Point(542, 55);
        LblEntries.Name = "LblEntries";
        LblEntries.Size = new Size(90, 24);
        LblEntries.TabIndex = 6;
        LblEntries.Text = "Watched:";
        // 
        // ListBoxWindows
        // 
        ListBoxWindows.FormattingEnabled = true;
        ListBoxWindows.ItemHeight = 15;
        ListBoxWindows.Location = new Point(10, 80);
        ListBoxWindows.Name = "ListBoxWindows";
        ListBoxWindows.Size = new Size(256, 244);
        ListBoxWindows.TabIndex = 0;
        ListBoxWindows.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        // 
        // ListBoxTabs
        // 
        ListBoxTabs.FormattingEnabled = true;
        ListBoxTabs.ItemHeight = 15;
        ListBoxTabs.Location = new Point(276, 80);
        ListBoxTabs.Name = "ListBoxTabs";
        ListBoxTabs.Size = new Size(256, 244);
        ListBoxTabs.TabIndex = 0;
        ListBoxTabs.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        // 
        // ListBoxEntries
        // 
        ListBoxEntries.FormattingEnabled = true;
        ListBoxEntries.ItemHeight = 15;
        ListBoxEntries.Location = new Point(542, 80);
        ListBoxEntries.Name = "ListBoxEntries";
        ListBoxEntries.Size = new Size(256, 244);
        ListBoxEntries.TabIndex = 0;
        ListBoxEntries.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        // 
        // BtnRefreshWindows
        // 
        BtnRefreshWindows.BackgroundImage = (Image)resources.GetObject("BtnRefreshWindows.BackgroundImage");
        BtnRefreshWindows.BackgroundImageLayout = ImageLayout.Stretch;
        BtnRefreshWindows.Location = new Point(130, 50);
        BtnRefreshWindows.Name = "BtnRefreshWindows";
        BtnRefreshWindows.Size = new Size(28, 28);
        BtnRefreshWindows.TabIndex = 1;
        BtnRefreshWindows.UseVisualStyleBackColor = true;
        BtnRefreshWindows.Click += BtnRefreshWindows_Click;
        // 
        // BtnRefreshTabs
        // 
        BtnRefreshTabs.BackgroundImage = (Image)resources.GetObject("BtnRefreshTabs.BackgroundImage");
        BtnRefreshTabs.BackgroundImageLayout = ImageLayout.Stretch;
        BtnRefreshTabs.Location = new Point(335, 50);
        BtnRefreshTabs.Name = "BtnRefreshTabs";
        BtnRefreshTabs.Size = new Size(28, 28);
        BtnRefreshTabs.TabIndex = 1;
        BtnRefreshTabs.UseVisualStyleBackColor = true;
        BtnRefreshTabs.Click += BtnRefreshTabs_Click;
        // 
        // BtnAdd
        // 
        BtnAdd.Location = new Point(365, 339);
        BtnAdd.Name = "BtnAdd";
        BtnAdd.Size = new Size(100, 25);
        BtnAdd.TabIndex = 1;
        BtnAdd.Text = "Add Entry";
        BtnAdd.UseVisualStyleBackColor = true;
        BtnAdd.Click += BtnAdd_Click;
        // 
        // BtnRemove
        // 
        BtnRemove.Location = new Point(695, 327);
        BtnRemove.Name = "BtnRemove";
        BtnRemove.Size = new Size(100, 30);
        BtnRemove.TabIndex = 1;
        BtnRemove.Text = "Remove Entry";
        BtnRemove.UseVisualStyleBackColor = true;
        BtnRemove.Click += BtnRemove_Click;
        // 
        // TextBoxEntryEditor
        // 
        TextBoxEntryEditor.Location = new Point(10, 340);
        TextBoxEntryEditor.Name = "TextBoxEntryEditor";
        TextBoxEntryEditor.Size = new Size(350, 23);
        TextBoxEntryEditor.TabIndex = 2;
        // 
        // GroupLiteralRegex
        // 
        GroupLiteralRegex.Controls.Add(RbLiteral);
        GroupLiteralRegex.Controls.Add(RbRegex);
        GroupLiteralRegex.Location = new Point(10, 375);
        GroupLiteralRegex.Name = "GroupLiteralRegex";
        GroupLiteralRegex.Size = new Size(120, 70);
        GroupLiteralRegex.TabIndex = 0;
        GroupLiteralRegex.TabStop = false;
        GroupLiteralRegex.Text = "String Type";
        // 
        // RbLiteral
        // 
        RbLiteral.Checked = true;
        RbLiteral.Location = new Point(15, 20);
        RbLiteral.Name = "RbLiteral";
        RbLiteral.Size = new Size(104, 24);
        RbLiteral.TabIndex = 4;
        RbLiteral.TabStop = true;
        RbLiteral.Text = "Literal";
        RbLiteral.UseVisualStyleBackColor = true;
        // 
        // RbRegex
        // 
        RbRegex.Location = new Point(15, 40);
        RbRegex.Name = "RbRegex";
        RbRegex.Size = new Size(104, 24);
        RbRegex.TabIndex = 4;
        RbRegex.Text = "Regex";
        RbRegex.UseVisualStyleBackColor = true;
        // 
        // GroupAppTab
        // 
        GroupAppTab.Controls.Add(RbApplication);
        GroupAppTab.Controls.Add(RbTab);
        GroupAppTab.Location = new Point(150, 375);
        GroupAppTab.Name = "GroupAppTab";
        GroupAppTab.Size = new Size(150, 68);
        GroupAppTab.TabIndex = 1;
        GroupAppTab.TabStop = false;
        GroupAppTab.Text = "Window Type";
        // 
        // RbApplication
        // 
        RbApplication.Checked = true;
        RbApplication.Location = new Point(15, 20);
        RbApplication.Name = "RbApplication";
        RbApplication.Size = new Size(104, 24);
        RbApplication.TabIndex = 4;
        RbApplication.TabStop = true;
        RbApplication.Text = "Application";
        RbApplication.UseVisualStyleBackColor = true;
        // 
        // RbTab
        // 
        RbTab.Location = new Point(15, 40);
        RbTab.Name = "RbTab";
        RbTab.Size = new Size(104, 24);
        RbTab.TabIndex = 4;
        RbTab.Text = "Browser Tab";
        RbTab.UseVisualStyleBackColor = true;
        // 
        // menuMain
        // 
        menuMain.Items.AddRange(new ToolStripItem[] { toolStripMenuItemFile });
        menuMain.Location = new Point(0, 0);
        menuMain.Name = "menuMain";
        menuMain.Size = new Size(808, 24);
        menuMain.TabIndex = 7;
        menuMain.Text = "menuStrip1";
        // 
        // toolStripMenuItemFile
        // 
        toolStripMenuItemFile.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, exitToolStripMenuItem });
        toolStripMenuItemFile.Name = "toolStripMenuItemFile";
        toolStripMenuItemFile.Size = new Size(37, 20);
        toolStripMenuItemFile.Text = "&File";
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new Size(180, 22);
        settingsToolStripMenuItem.Text = "&Settings";
        settingsToolStripMenuItem.Click += MenuItemSettings_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(180, 22);
        exitToolStripMenuItem.Text = "E&xit";
        exitToolStripMenuItem.Click += MenuItemExit_Click;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(808, 454);
        Controls.Add(GroupLiteralRegex);
        Controls.Add(GroupAppTab);
        Controls.Add(LblStatusMqtt);
        Controls.Add(LblMqtt);
        Controls.Add(LblOpenWindows);
        Controls.Add(LblOpenTabs);
        Controls.Add(LblEntries);
        Controls.Add(BtnRefreshWindows);
        Controls.Add(BtnRefreshTabs);
        Controls.Add(TextBoxEntryEditor);
        Controls.Add(BtnAdd);
        Controls.Add(BtnRemove);
        Controls.Add(ListBoxWindows);
        Controls.Add(ListBoxTabs);
        Controls.Add(ListBoxEntries);
        Controls.Add(menuMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = Properties.Resources.NotifyIcon_Icon;
        MainMenuStrip = menuMain;
        MaximizeBox = false;
        Name = "Main";
        Text = "HAcomms";
        Closing += Main_Closing;
        Shown += Main_Shown;
        GroupLiteralRegex.ResumeLayout(false);
        GroupAppTab.ResumeLayout(false);
        menuMain.ResumeLayout(false);
        menuMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label LblStatusMqtt;
    private System.Windows.Forms.Label LblMqtt;
    private System.Windows.Forms.Label LblOpenWindows;
    private System.Windows.Forms.Label LblOpenTabs;
    private System.Windows.Forms.Label LblEntries;
    private System.Windows.Forms.Button BtnRefreshWindows;
    private System.Windows.Forms.Button BtnRefreshTabs;
    private System.Windows.Forms.ListBox ListBoxWindows;
    private System.Windows.Forms.ListBox ListBoxTabs;
    private System.Windows.Forms.ListBox ListBoxEntries;
    private System.Windows.Forms.Button BtnAdd;
    private System.Windows.Forms.Button BtnRemove;
    private System.Windows.Forms.TextBox TextBoxEntryEditor;
    private System.Windows.Forms.GroupBox GroupLiteralRegex;
    private System.Windows.Forms.RadioButton RbRegex;
    private System.Windows.Forms.RadioButton RbLiteral;
    private System.Windows.Forms.GroupBox GroupAppTab;
    private System.Windows.Forms.RadioButton RbApplication;
    private System.Windows.Forms.RadioButton RbTab;
    private MenuStrip menuMain;
    private ToolStripMenuItem toolStripMenuItemFile;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
}