using System.Xml.Serialization;
using System.Diagnostics; // tmp

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

            InitializePetsDataGridView();


            UpdateButtonsState();
            LoadSettings();
        }

        private void InitializePetsDataGridView()
        {
            // Настраиваем DataGridView
            PetsDataGridView.AutoGenerateColumns = false;
            PetsDataGridView.Columns.Clear();

            // 1. Колонка # (ID)
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "#",
                Width = 50,
                Visible = true,
                ReadOnly = true
            });

            // 2. Колонка Кличка
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Alias",
                DataPropertyName = "Alias",
                HeaderText = "Кличка",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            // 3. Колонка Порода
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "BreedId",
                DataPropertyName = "BreedId",
                HeaderText = "Порода",
                Width = 100,
                ReadOnly = true
            });

            // 4. Колонка Вид
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TypeId",
                DataPropertyName = "TypeId",
                HeaderText = "Вид",
                Width = 80,
                ReadOnly = true
            });

            // 5. Колонка Пол
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Sex",
                DataPropertyName = "Sex",
                HeaderText = "Пол",
                Width = 80,
                ReadOnly = true
            });

            // 6. Колонка Дата рождения
            PetsDataGridView.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Birthday",
                DataPropertyName = "Birthday",
                HeaderText = "Дата рожд.",
                Width = 100,
                ReadOnly = true
            });

            // Настраиваем стиль заголовков
            PetsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            PetsDataGridView.ColumnHeadersHeight = 35;
            PetsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Настраиваем стиль строк
            PetsDataGridView.GridColor = SystemColors.ControlDark; // Цвет линий сетки
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

                    var pets = await _apiService.GetPetsByClientIdAsync(selectedClient.Id);

                    // Просто устанавливаем источник данных
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
