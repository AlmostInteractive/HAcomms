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
            lblMqttUsername = new Label();
            TbMqttUsername = new TextBox();
            label1 = new Label();
            TbMqttPassword = new TextBox();
            SuspendLayout();
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(12, 212);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(100, 25);
            BtnCancel.TabIndex = 2;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(272, 212);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(100, 25);
            BtnSave.TabIndex = 3;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // TbMqttAddress
            // 
            TbMqttAddress.Location = new Point(12, 35);
            TbMqttAddress.Name = "TbMqttAddress";
            TbMqttAddress.Size = new Size(360, 23);
            TbMqttAddress.TabIndex = 4;
            // 
            // LblMqttAddress
            // 
            LblMqttAddress.AccessibleDescription = "";
            LblMqttAddress.AccessibleName = "";
            LblMqttAddress.AccessibleRole = AccessibleRole.StaticText;
            LblMqttAddress.Location = new Point(12, 18);
            LblMqttAddress.Name = "LblMqttAddress";
            LblMqttAddress.Size = new Size(100, 14);
            LblMqttAddress.TabIndex = 5;
            LblMqttAddress.Text = "MQTT Address:";
            // 
            // lblMqttUsername
            // 
            lblMqttUsername.AccessibleDescription = "";
            lblMqttUsername.AccessibleName = "";
            lblMqttUsername.AccessibleRole = AccessibleRole.StaticText;
            lblMqttUsername.Location = new Point(12, 75);
            lblMqttUsername.Name = "lblMqttUsername";
            lblMqttUsername.Size = new Size(100, 14);
            lblMqttUsername.TabIndex = 7;
            lblMqttUsername.Text = "MQTT Username:";
            // 
            // TbMqttUsername
            // 
            TbMqttUsername.Location = new Point(12, 92);
            TbMqttUsername.Name = "TbMqttUsername";
            TbMqttUsername.Size = new Size(360, 23);
            TbMqttUsername.TabIndex = 6;
            // 
            // label1
            // 
            label1.AccessibleDescription = "";
            label1.AccessibleName = "";
            label1.AccessibleRole = AccessibleRole.StaticText;
            label1.Location = new Point(12, 132);
            label1.Name = "label1";
            label1.Size = new Size(100, 14);
            label1.TabIndex = 9;
            label1.Text = "MQTT Password:";
            // 
            // TbMqttPassword
            // 
            TbMqttPassword.Location = new Point(12, 149);
            TbMqttPassword.Name = "TbMqttPassword";
            TbMqttPassword.Size = new Size(360, 23);
            TbMqttPassword.TabIndex = 8;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 255);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(TbMqttPassword);
            Controls.Add(lblMqttUsername);
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
        private Label lblMqttUsername;
        private TextBox TbMqttUsername;
        private Label label1;
        private TextBox TbMqttPassword;
    }
}