using System.Windows.Forms.PropertyGridInternal;

namespace HAcomms;

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
    private void InitializeComponent() {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        this.components = new System.ComponentModel.Container();
        this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
        this.LblStatusMqtt = new System.Windows.Forms.Label();
        this.LblMqtt = new System.Windows.Forms.Label();
        this.LblOpenWindows = new System.Windows.Forms.Label();
        this.LblOpenTabs = new System.Windows.Forms.Label();
        this.LblEntries = new System.Windows.Forms.Label();
        this.ListBoxWindows = new System.Windows.Forms.ListBox();
        this.ListBoxTabs = new System.Windows.Forms.ListBox();
        this.ListBoxEntries = new System.Windows.Forms.ListBox();
        this.BtnRefreshWindows = new System.Windows.Forms.Button();
        this.BtnRefreshTabs = new System.Windows.Forms.Button();
        this.BtnAdd = new System.Windows.Forms.Button();
        this.BtnRemove = new System.Windows.Forms.Button();
        this.TextBoxEntryEditor = new System.Windows.Forms.TextBox();
        this.GroupLiteralRegex = new System.Windows.Forms.GroupBox();
        this.RbRegex = new System.Windows.Forms.RadioButton();
        this.RbLiteral = new System.Windows.Forms.RadioButton();
        this.GroupAppTab = new System.Windows.Forms.GroupBox();
        this.RbApplication = new System.Windows.Forms.RadioButton();
        this.RbTab = new System.Windows.Forms.RadioButton();
        
        this.GroupLiteralRegex.SuspendLayout();
        this.GroupAppTab.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // NotifyIcon
        // 
        this.NotifyIcon.BalloonTipTitle = "HAcomms";
        this.NotifyIcon.Icon = (System.Drawing.Icon)(Properties.Resources.NotifyIcon_Icon);
        this.NotifyIcon.Text = "HAcomms";
        this.NotifyIcon.BalloonTipTitle = "HAcomms";
        this.NotifyIcon.Visible = true;
        this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
        this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
        
        // 
        // LblMqtt
        // 
        this.LblMqtt.AccessibleDescription = "Description of the MQTT client.";
        this.LblMqtt.AccessibleName = "MQTT  description";
        this.LblMqtt.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblMqtt.Location = new System.Drawing.Point(10, 10);
        this.LblMqtt.Name = "LblMqtt";
        this.LblMqtt.Text = "MQTT:";
        // 
        // LblStatusMqtt
        // 
        this.LblStatusMqtt.AccessibleDescription = "MQTT status.";
        this.LblStatusMqtt.AccessibleName = "MQTT status";
        this.LblStatusMqtt.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblStatusMqtt.AutoSize = true;
        this.LblStatusMqtt.Location = new System.Drawing.Point(50, 10);
        this.LblStatusMqtt.Name = "LblStatusMqtt";
        this.LblStatusMqtt.Text = "-";
        // 
        // LblOpenWindows
        // 
        this.LblOpenWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LblOpenWindows.AccessibleDescription = "Running applications.";
        this.LblOpenWindows.AccessibleName = "Running applicaitons";
        this.LblOpenWindows.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblOpenWindows.AutoSize = true;
        this.LblOpenWindows.Location = new System.Drawing.Point(10, 35);
        this.LblOpenWindows.Name = "LblOpenWindows";
        this.LblOpenWindows.Text = "Applications:";
        // 
        // LblOpenTabs
        // 
        this.LblOpenTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LblOpenTabs.AccessibleDescription = "Browser tabs.";
        this.LblOpenTabs.AccessibleName = "Browser tabs";
        this.LblOpenTabs.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblOpenTabs.AutoSize = true;
        this.LblOpenTabs.Location = new System.Drawing.Point(276, 35);
        this.LblOpenTabs.Name = "LblOpenTabs";
        this.LblOpenTabs.Text = "Tabs:";
        // 
        // LblEntries
        // 
        this.LblEntries.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LblEntries.AccessibleDescription = "Watched apps and tabs.";
        this.LblEntries.AccessibleName = "Watched apps and tabs";
        this.LblEntries.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblEntries.AutoSize = true;
        this.LblEntries.Location = new System.Drawing.Point(542, 35);
        this.LblEntries.Name = "LblEntries";
        this.LblEntries.Text = "Watched:";
        // 
        // BtnRefreshWindows
        // 
        this.BtnRefreshWindows.Location = new System.Drawing.Point(130, 30);
        this.BtnRefreshWindows.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.BackgroundImage")));
        this.BtnRefreshWindows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.BtnRefreshWindows.Name = "BtnRefreshWindows";
        this.BtnRefreshWindows.Size = new System.Drawing.Size(28, 28);
        this.BtnRefreshWindows.TabIndex = 1;
        this.BtnRefreshWindows.Text = "";
        this.BtnRefreshWindows.UseVisualStyleBackColor = true;
        this.BtnRefreshWindows.Click += new System.EventHandler(this.BtnRefreshWindows_Click);
        // 
        // BtnRefreshTabs
        // 
        this.BtnRefreshTabs.Location = new System.Drawing.Point(335, 30);
        this.BtnRefreshTabs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.BackgroundImage")));
        this.BtnRefreshTabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.BtnRefreshTabs.Name = "BtnRefreshTabs";
        this.BtnRefreshTabs.Size = new System.Drawing.Size(28, 28);
        this.BtnRefreshTabs.TabIndex = 1;
        this.BtnRefreshTabs.Text = "";
        this.BtnRefreshTabs.UseVisualStyleBackColor = true;
        this.BtnRefreshTabs.Click += new System.EventHandler(this.BtnRefreshTabs_Click);
        // 
        // ListBoxWindows
        // 
        this.ListBoxWindows.FormattingEnabled = true;
        this.ListBoxWindows.Location = new System.Drawing.Point(10, 60);
        this.ListBoxWindows.Name = "ListBoxWindows";
        this.ListBoxWindows.Size = new System.Drawing.Size(256, 256);
        this.ListBoxWindows.TabIndex = 0;
        this.ListBoxWindows.SelectedIndexChanged += new System.EventHandler(this.ListBoxWindowsTabs_SelectedIndexChanged);
        // 
        // ListBoxTabs
        // 
        this.ListBoxTabs.FormattingEnabled = true;
        this.ListBoxTabs.Location = new System.Drawing.Point(276, 60);
        this.ListBoxTabs.Name = "ListBoxTabs";
        this.ListBoxTabs.Size = new System.Drawing.Size(256, 256);
        this.ListBoxTabs.TabIndex = 0;
        this.ListBoxTabs.SelectedIndexChanged += new System.EventHandler(this.ListBoxWindowsTabs_SelectedIndexChanged);
        // 
        // ListBoxEntries
        // 
        this.ListBoxEntries.FormattingEnabled = true;
        this.ListBoxEntries.Location = new System.Drawing.Point(542, 60);
        this.ListBoxEntries.Name = "ListBoxEntries";
        this.ListBoxEntries.Size = new System.Drawing.Size(256, 256);
        this.ListBoxEntries.TabIndex = 0;
        // 
        // TextBoxEntryEditor
        // 
        this.TextBoxEntryEditor.Location = new System.Drawing.Point(10, 320);
        this.TextBoxEntryEditor.Name = "TextBoxEntryEditor";
        this.TextBoxEntryEditor.Size = new System.Drawing.Size(350, 20);
        this.TextBoxEntryEditor.TabIndex = 2;
        // 
        // BtnAdd
        // 
        this.BtnAdd.Location = new System.Drawing.Point(365, 319);
        this.BtnAdd.Name = "BtnAdd";
        this.BtnAdd.Size = new System.Drawing.Size(100, 25);
        this.BtnAdd.TabIndex = 1;
        this.BtnAdd.Text = "Add Entry";
        this.BtnAdd.UseVisualStyleBackColor = true;
        this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
        // 
        // GroupLiteralRegex
        // 
        this.GroupLiteralRegex.Controls.Add(this.RbLiteral);
        this.GroupLiteralRegex.Controls.Add(this.RbRegex);
        this.GroupLiteralRegex.Location = new System.Drawing.Point(10, 355);
        this.GroupLiteralRegex.Name = "GroupLiteralRegex";
        this.GroupLiteralRegex.Size = new System.Drawing.Size(120,70);
        this.GroupLiteralRegex.TabStop = false;
        this.GroupLiteralRegex.Text = "String Type";
        // this.GroupLiteralRegex.Visible = false;
        // 
        // RbLiteral
        // 
        this.RbLiteral.Location = new System.Drawing.Point(15, 20);
        this.RbLiteral.Name = "RbLiteral";
        this.RbLiteral.Size = new System.Drawing.Size(104, 24);
        this.RbLiteral.TabIndex = 4;
        this.RbLiteral.Text = "Literal";
        this.RbLiteral.UseVisualStyleBackColor = true;
        this.RbLiteral.Checked = true;
        // 
        // RbRegex
        // 
        this.RbRegex.Location = new System.Drawing.Point(15, 40);
        this.RbRegex.Name = "RbRegex";
        this.RbRegex.Size = new System.Drawing.Size(104, 24);
        this.RbRegex.TabIndex = 4;
        this.RbRegex.Text = "Regex";
        this.RbRegex.UseVisualStyleBackColor = true;
        this.RbRegex.Checked = false;
        // 
        // GroupAppTab
        // 
        this.GroupAppTab.Controls.Add(this.RbApplication);
        this.GroupAppTab.Controls.Add(this.RbTab);
        this.GroupAppTab.Location = new System.Drawing.Point(150, 355);
        this.GroupAppTab.Name = "GroupAppTab";
        this.GroupAppTab.Size = new System.Drawing.Size(150, 68);
        this.GroupAppTab.TabStop = false;
        this.GroupAppTab.Text = "Window Type";
        // this.GroupAppTab.Visible = false;
        // 
        // RbApplication
        // 
        this.RbApplication.Location = new System.Drawing.Point(15, 20);
        this.RbApplication.Name = "RbApplication";
        this.RbApplication.Size = new System.Drawing.Size(104, 24);
        this.RbApplication.TabIndex = 4;
        this.RbApplication.Text = "Application";
        this.RbApplication.UseVisualStyleBackColor = true;
        this.RbApplication.Checked = true;
        // 
        // RbTab
        // 
        this.RbTab.Location = new System.Drawing.Point(15, 40);
        this.RbTab.Name = "RbTab";
        this.RbTab.Size = new System.Drawing.Size(104, 24);
        this.RbTab.TabIndex = 4;
        this.RbTab.Text = "Browser Tab";
        this.RbTab.UseVisualStyleBackColor = true;
        this.RbTab.Checked = false;
        // 
        // BtnRemove
        // 
        this.BtnRemove.Location = new System.Drawing.Point(695, 307);
        this.BtnRemove.Name = "BtnRemove";
        this.BtnRemove.Size = new System.Drawing.Size(100, 30);
        this.BtnRemove.TabIndex = 1;
        this.BtnRemove.Text = "Remove Entry";
        this.BtnRemove.UseVisualStyleBackColor = true;

        // 
        // Main
        //
        this.Icon = (System.Drawing.Icon)(Properties.Resources.NotifyIcon_Icon);
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(808, 435);
        this.Text = "HAcomms";
        this.Shown += new EventHandler(this.Main_Shown);
        this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
        this.Controls.Add(this.GroupLiteralRegex);
        this.Controls.Add(this.GroupAppTab);
        this.Controls.Add(this.LblStatusMqtt);
        this.Controls.Add(this.LblMqtt);
        this.Controls.Add(this.LblOpenWindows);
        this.Controls.Add(this.LblOpenTabs);
        this.Controls.Add(this.LblEntries);
        this.Controls.Add(this.BtnRefreshWindows);
        this.Controls.Add(this.BtnRefreshTabs);
        this.Controls.Add(this.TextBoxEntryEditor);
        this.Controls.Add(this.BtnAdd);
        this.Controls.Add(this.BtnRemove);
        this.Controls.Add(this.ListBoxWindows);
        this.Controls.Add(this.ListBoxTabs);
        this.Controls.Add(this.ListBoxEntries);
        
        this.GroupLiteralRegex.ResumeLayout(false);
        this.GroupAppTab.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
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
}