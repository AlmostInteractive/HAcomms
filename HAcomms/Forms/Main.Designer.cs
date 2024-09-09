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
        contextMenu = new ContextMenuStrip(components);
        showToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem1 = new ToolStripMenuItem();
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
        BtnAddEntry = new Button();
        BtnRemoveEntry = new Button();
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
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        TextBoxComboId = new TextBox();
        TextBoxKeyCombo = new TextBox();
        BtnAddCombo = new Button();
        label4 = new Label();
        BtnRemoveCombo = new Button();
        ListBoxCombos = new ListBox();
        contextMenu.SuspendLayout();
        GroupLiteralRegex.SuspendLayout();
        GroupAppTab.SuspendLayout();
        menuMain.SuspendLayout();
        SuspendLayout();
        // 
        // NotifyIcon
        // 
        NotifyIcon.BalloonTipTitle = "HAcomms";
        NotifyIcon.ContextMenuStrip = contextMenu;
        NotifyIcon.Icon = Properties.Resources.AppIcon;
        NotifyIcon.Text = "HAcomms";
        NotifyIcon.Visible = true;
        NotifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        // 
        // contextMenu
        // 
        contextMenu.Items.AddRange(new ToolStripItem[] { showToolStripMenuItem, exitToolStripMenuItem1 });
        contextMenu.Name = "contextMenu";
        contextMenu.Size = new Size(104, 48);
        // 
        // showToolStripMenuItem
        // 
        showToolStripMenuItem.Name = "showToolStripMenuItem";
        showToolStripMenuItem.Size = new Size(103, 22);
        showToolStripMenuItem.Text = "&Show";
        showToolStripMenuItem.Click += ShowToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem1
        // 
        exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
        exitToolStripMenuItem1.Size = new Size(103, 22);
        exitToolStripMenuItem1.Text = "E&xit";
        exitToolStripMenuItem1.Click += ExitProgram;
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
        // BtnAddEntry
        // 
        BtnAddEntry.Location = new Point(366, 339);
        BtnAddEntry.Name = "BtnAddEntry";
        BtnAddEntry.Size = new Size(100, 25);
        BtnAddEntry.TabIndex = 1;
        BtnAddEntry.Text = "Add Entry";
        BtnAddEntry.UseVisualStyleBackColor = true;
        BtnAddEntry.Click += BtnAddEntry_Click;
        // 
        // BtnRemoveEntry
        // 
        BtnRemoveEntry.Location = new Point(698, 330);
        BtnRemoveEntry.Name = "BtnRemoveEntry";
        BtnRemoveEntry.Size = new Size(100, 30);
        BtnRemoveEntry.TabIndex = 1;
        BtnRemoveEntry.Text = "Remove Entry";
        BtnRemoveEntry.UseVisualStyleBackColor = true;
        BtnRemoveEntry.Click += BtnRemoveEntry_Click;
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
        settingsToolStripMenuItem.Size = new Size(116, 22);
        settingsToolStripMenuItem.Text = "&Settings";
        settingsToolStripMenuItem.Click += MenuItemSettings_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(116, 22);
        exitToolStripMenuItem.Text = "E&xit";
        exitToolStripMenuItem.Click += ExitProgram;
        // 
        // label1
        // 
        label1.AccessibleDescription = "Description of the MQTT client.";
        label1.AccessibleName = "MQTT  description";
        label1.AccessibleRole = AccessibleRole.StaticText;
        label1.Location = new Point(10, 497);
        label1.Name = "label1";
        label1.Size = new Size(100, 16);
        label1.TabIndex = 8;
        label1.Text = "Unique Id:";
        // 
        // label2
        // 
        label2.AccessibleDescription = "Running applications.";
        label2.AccessibleName = "Running applicaitons";
        label2.AccessibleRole = AccessibleRole.StaticText;
        label2.AutoSize = true;
        label2.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label2.Location = new Point(10, 467);
        label2.Name = "label2";
        label2.Size = new Size(225, 24);
        label2.TabIndex = 9;
        label2.Text = "Keyboard Combo Events:";
        // 
        // label3
        // 
        label3.AccessibleDescription = "Description of the MQTT client.";
        label3.AccessibleName = "MQTT  description";
        label3.AccessibleRole = AccessibleRole.StaticText;
        label3.Location = new Point(10, 524);
        label3.Name = "label3";
        label3.Size = new Size(100, 16);
        label3.TabIndex = 10;
        label3.Text = "Key Combo:";
        // 
        // TextBoxComboId
        // 
        TextBoxComboId.Location = new Point(87, 494);
        TextBoxComboId.Name = "TextBoxComboId";
        TextBoxComboId.Size = new Size(213, 23);
        TextBoxComboId.TabIndex = 11;
        // 
        // TextBoxKeyCombo
        // 
        TextBoxKeyCombo.Location = new Point(87, 521);
        TextBoxKeyCombo.Name = "TextBoxKeyCombo";
        TextBoxKeyCombo.Size = new Size(213, 23);
        TextBoxKeyCombo.TabIndex = 12;
        TextBoxKeyCombo.GotFocus += TextBoxKeyCombo_GotFocus;
        TextBoxKeyCombo.LostFocus += TextBoxKeyCombo_LostFocus;
        // 
        // BtnAddCombo
        // 
        BtnAddCombo.Location = new Point(306, 520);
        BtnAddCombo.Name = "BtnAddCombo";
        BtnAddCombo.Size = new Size(100, 25);
        BtnAddCombo.TabIndex = 13;
        BtnAddCombo.Text = "Add Combo";
        BtnAddCombo.UseVisualStyleBackColor = true;
        BtnAddCombo.Click += BtnAddCombo_Click;
        // 
        // label4
        // 
        label4.AccessibleDescription = "Watched apps and tabs.";
        label4.AccessibleName = "Watched apps and tabs";
        label4.AccessibleRole = AccessibleRole.StaticText;
        label4.AutoSize = true;
        label4.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label4.Location = new Point(442, 467);
        label4.Name = "label4";
        label4.Size = new Size(86, 24);
        label4.TabIndex = 16;
        label4.Text = "Combos:";
        // 
        // BtnRemoveCombo
        // 
        BtnRemoveCombo.Location = new Point(687, 639);
        BtnRemoveCombo.Name = "BtnRemoveCombo";
        BtnRemoveCombo.Size = new Size(111, 30);
        BtnRemoveCombo.TabIndex = 15;
        BtnRemoveCombo.Text = "Remove Combo";
        BtnRemoveCombo.UseVisualStyleBackColor = true;
        BtnRemoveCombo.Click += BtnRemoveCombo_Click;
        // 
        // ListBoxCombos
        // 
        ListBoxCombos.FormattingEnabled = true;
        ListBoxCombos.ItemHeight = 15;
        ListBoxCombos.Location = new Point(442, 494);
        ListBoxCombos.Name = "ListBoxCombos";
        ListBoxCombos.Size = new Size(356, 139);
        ListBoxCombos.TabIndex = 14;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(808, 678);
        Controls.Add(label4);
        Controls.Add(BtnRemoveCombo);
        Controls.Add(ListBoxCombos);
        Controls.Add(BtnAddCombo);
        Controls.Add(TextBoxKeyCombo);
        Controls.Add(TextBoxComboId);
        Controls.Add(label3);
        Controls.Add(label1);
        Controls.Add(label2);
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
        Controls.Add(BtnAddEntry);
        Controls.Add(BtnRemoveEntry);
        Controls.Add(ListBoxWindows);
        Controls.Add(ListBoxTabs);
        Controls.Add(ListBoxEntries);
        Controls.Add(menuMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = Properties.Resources.AppIcon;
        MainMenuStrip = menuMain;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "Main";
        Text = "HAcomms";
        Shown += Main_Shown;
        contextMenu.ResumeLayout(false);
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
    private System.Windows.Forms.Button BtnAddEntry;
    private System.Windows.Forms.Button BtnRemoveEntry;
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
    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox TextBoxComboId;
    private TextBox TextBoxKeyCombo;
    private Button BtnAddCombo;
    private Label label4;
    private Button BtnRemoveCombo;
    private ListBox ListBoxCombos;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem showToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem1;
}