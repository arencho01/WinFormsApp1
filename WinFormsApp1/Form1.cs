using System.Xml.Serialization;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private ApiSettings? _settings;
        private VetmanagerApiService? _apiService;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            APISettingsBtn.Click += APISettingsBtn_Click;
            EditBtn.Click += EditBtn_Click;
            AddBtn.Click += AddBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            ClientComboBox.SelectedIndexChanged += ClientСomboBox_SelectedIndexChanged;
            PetsDataGridView.SelectionChanged += PetsDataGridView_SelectionChanged;

            UpdateButtonsState();
            LoadSettings();
        }

        private void PetsDataGridView_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateButtonsState();
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

        private async void ClientСomboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ClientComboBox.SelectedItem is Client selectedClient && _apiService != null)
            {
                try
                {
                    PetsDataGridView.Enabled = false;
                    PetsDataGridView.DataSource = null;

                    // Загружаем питомцев выбранного клиента
                    var pets = await _apiService.GetPetsByClientIdAsync(selectedClient.Id);

                    // Настраиваем DataGridView
                    PetsDataGridView.AutoGenerateColumns = false;
                    PetsDataGridView.Columns.Clear();

                    // Добавляем колонки
                    PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = "Id",
                        DataPropertyName = "Id",
                        HeaderText = "ID",
                        Width = 50,
                        Visible = false // Скрываем ID
                    });

                    PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = "Alias",
                        DataPropertyName = "Alias",
                        HeaderText = "Кличка",
                        Width = 150
                    });

                    PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = "Sex",
                        DataPropertyName = "Sex",
                        HeaderText = "Пол",
                        Width = 100
                    });

                    // Устанавливаем источник данных
                    PetsDataGridView.DataSource = pets;
                    PetsDataGridView.Enabled = true;
                }
                catch (Exception)
                {
                    PetsDataGridView.DataSource = null;
                    PetsDataGridView.Enabled = true;
                }
            }
            else
            {
                // Если клиент не выбран, очищаем таблицу
                PetsDataGridView.DataSource = null;
            }

            UpdateButtonsState();
        }

        private void LoadSettings()
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

                        LoadClientsAsync();
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
            // Кнопка Добавить активна, когда выбран клиент
            AddBtn.Enabled = ClientComboBox.SelectedItem != null;

            // Кнопки Редактировать и Удалить активны, когда выбран питомец
            bool hasSelectedPet = PetsDataGridView.SelectedRows.Count > 0;
            EditBtn.Enabled = hasSelectedPet;
            DeleteBtn.Enabled = hasSelectedPet;
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
    }
}
