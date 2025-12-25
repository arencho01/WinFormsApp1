using System.Xml.Serialization;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private ApiSettings? _settings;
        private VetmanagerApiService? _apiService;
        private DataGridView? _petsGridView;

        private List<PetType> _petTypes = new();
        private List<Breed> _breeds = new();

        private Client? _selectedClient;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            APISettingsBtn.Click += APISettingsBtn_Click;
            EditBtn.Click += EditBtn_Click;
            AddBtn.Click += AddBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            ClientComboBox.SelectedIndexChanged += ClientСomboBox_SelectedIndexChanged;

            InitializePetsGrid();
            UpdateButtonsState();
            LoadSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void APISettingsBtn_Click(Object? sender, EventArgs e)
        {
            APISettingsForm APISettingsForm = new APISettingsForm();

            APISettingsForm.ShowDialog();

            LoadSettings();
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

        private async void LoadSettings()
        {
            try
            {
                if (File.Exists("settings.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ApiSettings));

                    using var reader = new StreamReader("settings.xml");

                    var result = serializer.Deserialize(reader);

                    if (result is ApiSettings loadedSettings)
                    {
                        _settings = loadedSettings;

                        _apiService = new VetmanagerApiService(_settings.Domain, _settings.Token);

                        ClientComboBox.Enabled = true;

                        await LoadClientsAsync();
                        await LoadReferenceDataAsync();

                    }
                    else
                    {
                        MessageBox.Show("Файл настроек поврежден", "Ошибка", MessageBoxButtons.OK);
                        _settings = new ApiSettings();
                        ClientComboBox.Enabled = false;
                    }
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

        private void UpdateButtonsState()
        {
            AddBtn.Enabled = false;
            EditBtn.Enabled = false;
            DeleteBtn.Enabled = false;
        }

        private async void LoadClientsAsync()
        {
            try
            {
                ClientComboBox.Enabled = false;
                ClientComboBox.Text = "Загрузка клиентов...";

                if (_apiService == null)
                {
                    MessageBox.Show("API сервис не инициализирован", "Ошибка");
                    return;
                }

                var clients = await _apiService.GetClientsAsync();

                ClientComboBox.BeginUpdate();
                ClientComboBox.Items.Clear();

                foreach (var client in clients)
                {
                    ClientComboBox.Items.Add(client);
                }

                ClientComboBox.EndUpdate();
                ClientComboBox.Enabled = true;
                ClientComboBox.Text = "";

                MessageBox.Show($"Загружено клиентов: {clients.Length}", "Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка");
                ClientComboBox.Enabled = true;
                ClientComboBox.Text = "Ошибка загрузки";
            }
        }

        private void InitializePetsGrid()
        {
            _petsGridView = new DataGridView
            {
                Location = new Point(12, 110),
                Size = new Size(776, 388),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            _petsGridView.Columns.Add("Id", "ID");
            _petsGridView.Columns.Add("Alias", "Кличка");
            _petsGridView.Columns.Add("TypeName", "Вид");
            _petsGridView.Columns.Add("BreedName", "Порода");
            _petsGridView.Columns.Add("Sex", "Пол");

            _petsGridView.Columns["Id"].Visible = false;
            _petsGridView.SelectionChanged += PetsGridView_SelectionChanged;
            Controls.Add(_petsGridView);
        }

        private void PetsGridView_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateButtonsState();
        }
    }
}
