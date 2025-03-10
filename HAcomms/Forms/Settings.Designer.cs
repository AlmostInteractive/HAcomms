namespace HAcomms.Forms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnCancel = new Button();
            BtnSave = new Button();
            TbMqttAddress = new TextBox();
            LblMqttAddress = new Label();
            LblMqttUsername = new Label();
            TbMqttUsername = new TextBox();
            LblMqttPassword = new Label();
            TbMqttPassword = new TextBox();
            LblClientId = new Label();
            TbMqttClientId = new TextBox();
            SuspendLayout();
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(12, 269);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(100, 25);
            BtnCancel.TabIndex = 2;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(272, 269);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(100, 25);
            BtnSave.TabIndex = 3;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // TbMqttAddress
            // 
            TbMqttAddress.Location = new Point(12, 92);
            TbMqttAddress.Name = "TbMqttAddress";
            TbMqttAddress.Size = new Size(360, 23);
            TbMqttAddress.TabIndex = 4;
            // 
            // LblMqttAddress
            // 
            LblMqttAddress.AccessibleDescription = "";
            LblMqttAddress.AccessibleName = "";
            LblMqttAddress.AccessibleRole = AccessibleRole.StaticText;
            LblMqttAddress.Location = new Point(12, 75);
            LblMqttAddress.Name = "LblMqttAddress";
            LblMqttAddress.Size = new Size(100, 14);
            LblMqttAddress.TabIndex = 5;
            LblMqttAddress.Text = "MQTT Address:";
            // 
            // lblMqttUsername
            // 
            LblMqttUsername.AccessibleDescription = "";
            LblMqttUsername.AccessibleName = "";
            LblMqttUsername.AccessibleRole = AccessibleRole.StaticText;
            LblMqttUsername.Location = new Point(12, 132);
            LblMqttUsername.Name = "LblMqttUsername";
            LblMqttUsername.Size = new Size(100, 14);
            LblMqttUsername.TabIndex = 7;
            LblMqttUsername.Text = "MQTT Username:";
            // 
            // TbMqttUsername
            // 
            TbMqttUsername.Location = new Point(12, 149);
            TbMqttUsername.Name = "TbMqttUsername";
            TbMqttUsername.Size = new Size(360, 23);
            TbMqttUsername.TabIndex = 6;
            // 
            // label1
            // 
            LblMqttPassword.AccessibleDescription = "";
            LblMqttPassword.AccessibleName = "";
            LblMqttPassword.AccessibleRole = AccessibleRole.StaticText;
            LblMqttPassword.Location = new Point(12, 189);
            LblMqttPassword.Name = "LblMqttPassword";
            LblMqttPassword.Size = new Size(100, 14);
            LblMqttPassword.TabIndex = 9;
            LblMqttPassword.Text = "MQTT Password:";
            // 
            // TbMqttPassword
            // 
            TbMqttPassword.Location = new Point(12, 206);
            TbMqttPassword.Name = "TbMqttPassword";
            TbMqttPassword.Size = new Size(360, 23);
            TbMqttPassword.TabIndex = 8;
            // 
            // label2
            // 
            LblClientId.AccessibleDescription = "";
            LblClientId.AccessibleName = "";
            LblClientId.AccessibleRole = AccessibleRole.StaticText;
            LblClientId.Location = new Point(12, 18);
            LblClientId.Name = "TbMqttClientId";
            LblClientId.Size = new Size(100, 17);
            LblClientId.TabIndex = 11;
            LblClientId.Text = "Client Id:";
            // 
            // textBox1
            // 
            TbMqttClientId.Location = new Point(12, 35);
            TbMqttClientId.Name = "TbMqttClientId";
            TbMqttClientId.Size = new Size(360, 23);
            TbMqttClientId.TabIndex = 10;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 314);
            ControlBox = false;
            Controls.Add(LblClientId);
            Controls.Add(TbMqttClientId);
            Controls.Add(LblMqttPassword);
            Controls.Add(TbMqttPassword);
            Controls.Add(LblMqttUsername);
            Controls.Add(TbMqttUsername);
            Controls.Add(LblMqttAddress);
            Controls.Add(TbMqttAddress);
            Controls.Add(BtnSave);
            Controls.Add(BtnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnCancel;
        private Button BtnSave;
        private TextBox TbMqttAddress;
        private Label LblMqttAddress;
        private Label LblMqttUsername;
        private TextBox TbMqttUsername;
        private Label LblMqttPassword;
        private TextBox TbMqttPassword;
        private Label LblClientId;
        private TextBox TbMqttClientId;
    }
}