namespace HAcomms.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            
            string address = Properties.Settings.Default.MqttBroker_Address ?? "";
            string username = Properties.Settings.Default.MqttBroker_Username ?? "";
            string password = Properties.Settings.Default.MqttBroker_Password ?? "";

            this.TbMqttAddress.Text = address;
            this.TbMqttUsername.Text = username;
            this.TbMqttPassword.Text = password;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            Properties.Settings.Default.MqttBroker_Address = this.TbMqttAddress.Text.Trim();
            Properties.Settings.Default.MqttBroker_Username = this.TbMqttUsername.Text.Trim();
            Properties.Settings.Default.MqttBroker_Password = this.TbMqttPassword.Text.Trim();
            Properties.Settings.Default.Save();
            var main = this.Owner as Main;
            main?.ReloadSettings();
            this.Close();
        }
    }
}
