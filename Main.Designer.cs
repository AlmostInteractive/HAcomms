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
        
        Label label1 = new Label();

        // Set the border to a three-dimensional border.
        label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        // Align the image to the top left corner.
        label1.ImageAlign = ContentAlignment.TopLeft;
        // Specify that the text can display mnemonic characters.
        label1.UseMnemonic = true;
        // Set the text of the control and specify a mnemonic character.
        label1.Text = "First &Name:";
   
        /* Set the size of the control based on the PreferredHeight and PreferredWidth values. */
        label1.Size = new Size (label1.PreferredWidth, label1.PreferredHeight);
        
        // 
        // LblMqtt
        // 
        this.LblMqtt.AccessibleDescription = "Description of the MQTT client.";
        this.LblMqtt.AccessibleName = "MQTT  description";
        this.LblMqtt.AccessibleRole = System.Windows.Forms.AccessibleRole.StaticText;
        this.LblMqtt.Location = new System.Drawing.Point(10, 10);
        this.LblMqtt.Name = "LblMqtt";
        this.LblMqtt.TabIndex = 1;
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
        this.LblStatusMqtt.TabIndex = 2;
        this.LblStatusMqtt.Text = "-";
        
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
        // Main
        //
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(450, 600);
        this.Text = "HAcomms";
        this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Closing);
        this.Controls.Add(LblStatusMqtt);
        this.Controls.Add(LblMqtt);
    }

    #endregion
    
    private System.Windows.Forms.Label LblStatusMqtt;
    private System.Windows.Forms.Label LblMqtt;
}