namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private ApiSettings _settings;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            APISettingsBtn.Click += APISettingsBtn_Click;
            EditBtn.Click += EditBtn_Click;
            AddBtn.Click += AddBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            ClientComboBox.SelectedIndexChanged += ClientСomboBox_SelectedIndexChanged;

            LoadSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void APISettingsBtn_Click(Object? sender, EventArgs e)
        {
            APISettingsForm APISettingsForm = new APISettingsForm();
            APISettingsForm.Show();
        }

        private void EditBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Edit");
        }

        private void AddBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Add");
        }

        private void DeleteBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Delete");
        }

        private void ClientСomboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {

        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists("settings.xml"))
                {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(ApiSettings));

                    using var reader = new StreamReader("settings.xml");

                    _settings = (ApiSettings)serializer.Deserialize(reader);

                    ClientComboBox.Enabled = true;

                    MessageBox.Show("Настройки подключения загружены!", "Успешно", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Файл настроек не найден.\nНажмите 'Настройки API' для подключения", "Ошибка", MessageBoxButtons.OK);

                    ClientComboBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ошибка при загрузке настроек:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK);

                ClientComboBox.Enabled = false;
            }
        }
    }
}
