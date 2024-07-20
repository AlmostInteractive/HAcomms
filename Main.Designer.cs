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
        this.components = new System.ComponentModel.Container();
        this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
        this.LblStatusMqtt = new System.Windows.Forms.Label();
        this.LblMqtt = new System.Windows.Forms.Label();
        this.LblOpenWindows = new System.Windows.Forms.Label();
        this.LblEntries = new System.Windows.Forms.Label();
        this.ListBoxWindows = new System.Windows.Forms.ListBox();
        this.ListBoxEntries = new System.Windows.Forms.ListBox();
        this.BtnAdd = new System.Windows.Forms.Button();
        this.BtnRemove = new System.Windows.Forms.Button();
        this.TextBoxEntryEditor = new System.Windows.Forms.TextBox();
        
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
        // LblEntries
        // 
        this.LblEntries.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.LblEntries.AccessibleDescription = "Watched (regex).";
        this.LblEntries.AccessibleName = "Watched Application Regex";
        this.LblEntries.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblEntries.AutoSize = true;
        this.LblEntries.Location = new System.Drawing.Point(276, 35);
        this.LblEntries.Name = "LblEntries";
        this.LblEntries.Text = "Watched (regex):";
        // 
        // ListBoxWindows
        // 
        this.ListBoxWindows.FormattingEnabled = true;
        this.ListBoxWindows.Location = new System.Drawing.Point(10, 60);
        this.ListBoxWindows.Name = "ListBoxWindows";
        this.ListBoxWindows.Size = new System.Drawing.Size(256, 256);
        this.ListBoxWindows.TabIndex = 0;
        this.ListBoxWindows.SelectedIndexChanged += new System.EventHandler(this.ListBoxWindows_SelectedIndexChanged);
        // 
        // ListBoxEntries
        // 
        this.ListBoxEntries.FormattingEnabled = true;
        this.ListBoxEntries.Location = new System.Drawing.Point(276, 60);
        this.ListBoxEntries.Name = "ListBoxEntries";
        this.ListBoxEntries.Size = new System.Drawing.Size(256, 256);
        this.ListBoxEntries.TabIndex = 0;
        // 
        // TextBoxEntryEditor
        // 
        this.TextBoxEntryEditor.Location = new System.Drawing.Point(10, 320);
        this.TextBoxEntryEditor.Name = "TextBoxEntryEditor";
        this.TextBoxEntryEditor.Size = new System.Drawing.Size(256, 20);
        this.TextBoxEntryEditor.TabIndex = 2;
        // 
        // BtnAdd
        // 
        this.BtnAdd.Location = new System.Drawing.Point(10, 345);
        this.BtnAdd.Name = "BtnAdd";
        this.BtnAdd.Size = new System.Drawing.Size(100, 30);
        this.BtnAdd.TabIndex = 1;
        this.BtnAdd.Text = "Add Regex";
        this.BtnAdd.UseVisualStyleBackColor = true;
        // 
        // BtnRemove
        // 
        this.BtnRemove.Location = new System.Drawing.Point(433, 307);
        this.BtnRemove.Name = "BtnRemove";
        this.BtnRemove.Size = new System.Drawing.Size(100, 30);
        this.BtnRemove.TabIndex = 1;
        this.BtnRemove.Text = "Remove Regex";
        this.BtnRemove.UseVisualStyleBackColor = true;
        
        // 
        // Main
        //
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(600, 400);
        this.Text = "HAcomms";
        this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
        this.Controls.Add(LblStatusMqtt);
        this.Controls.Add(LblMqtt);
        this.Controls.Add(LblOpenWindows);
        this.Controls.Add(LblEntries);
        this.Controls.Add(this.TextBoxEntryEditor);
        this.Controls.Add(this.BtnAdd);
        this.Controls.Add(this.BtnRemove);
        this.Controls.Add(this.ListBoxWindows);
        this.Controls.Add(this.ListBoxEntries);
    }

    #endregion
    
    private System.Windows.Forms.Label LblStatusMqtt;
    private System.Windows.Forms.Label LblMqtt;
    private System.Windows.Forms.Label LblOpenWindows;
    private System.Windows.Forms.Label LblEntries;
    private System.Windows.Forms.ListBox ListBoxWindows;
    private System.Windows.Forms.ListBox ListBoxEntries;
    private System.Windows.Forms.Button BtnAdd;
    private System.Windows.Forms.Button BtnRemove;
    private System.Windows.Forms.TextBox TextBoxEntryEditor;
}