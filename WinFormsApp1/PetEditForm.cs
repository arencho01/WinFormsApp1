using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class PetEditForm : Form
    {
        public Pet? EditedPet { get; private set; }
        private readonly VetmanagerApiService _apiService;
        private readonly int _ownerId;
        private bool _isEditMode = false;

        public PetEditForm(VetmanagerApiService apiService, int ownerId, Pet? petToEdit = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _apiService = apiService;
            _ownerId = ownerId;

            // Настраиваем SexComboBox
            petSexComboBox.Items.AddRange(new object[] { "male", "female", "castrated", "sterilized", "unknown" });
            petSexComboBox.SelectedIndex = 0; // Устанавливаем значение по умолчанию

            if (petToEdit != null)
            {
                _isEditMode = true;
                EditedPet = petToEdit;
                this.Text = "Редактирование питомца";
                LoadPetData(petToEdit);
            }
            else
            {
                this.Text = "Добавление питомца";
            }

            saveBtn.Click += SaveBtn_Click;
            cancelBtn.Click += CancelBtn_Click;
            petTypeComboBox.SelectedIndexChanged += PetTypeComboBox_SelectedIndexChanged;

            this.Load += PetEditForm_Load;
        }

        private async void PetEditForm_Load(object? sender, EventArgs e)
        {
            await LoadPetTypesAsync();
        }

        private async Task LoadPetTypesAsync()
        {
            try
            {
                var petTypes = await _apiService.GetPetTypesAsync();

                // Для отладки
                MessageBox.Show($"Загружено видов: {petTypes.Length}", "Отладка");

                petTypeComboBox.BeginUpdate();
                petTypeComboBox.Items.Clear();

                foreach (var petType in petTypes)
                {
                    petTypeComboBox.Items.Add(petType);
                }

                petTypeComboBox.EndUpdate();

                if (petTypeComboBox.Items.Count > 0)
                {
                    petTypeComboBox.SelectedIndex = 0;
                    await LoadBreedsForSelectedTypeAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки видов: {ex.Message}", "Ошибка");
            }
        }

        private async void PetTypeComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await LoadBreedsForSelectedTypeAsync();
        }

        private async Task LoadBreedsForSelectedTypeAsync()
        {
            if (petTypeComboBox.SelectedItem is PetType selectedType)
            {
                try
                {
                    Debug.WriteLine($"Выбран вид: {selectedType.Title}, ID: {selectedType.Id}");

                    var breeds = await _apiService.GetBreedsByTypeIdAsync(selectedType.Id);

                    Debug.WriteLine($"Получено пород: {breeds.Length}");

                    petBreedComboBox.BeginUpdate();
                    petBreedComboBox.Items.Clear();

                    foreach (var breed in breeds)
                    {
                        petBreedComboBox.Items.Add(breed);
                        Debug.WriteLine($"Добавлена порода: {breed.Title}, PetTypeId: {breed.PetTypeId}");
                    }

                    petBreedComboBox.EndUpdate();

                    if (petBreedComboBox.Items.Count > 0)
                    {
                        petBreedComboBox.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки пород: {ex.Message}", "Ошибка");
                }
            }
        }

        private void LoadPetData(Pet pet)
        {
            petAliasTextBox.Text = pet.Alias;

            // Устанавливаем пол
            if (!string.IsNullOrEmpty(pet.Sex))
            {
                // Находим соответствующий элемент в ComboBox
                for (int i = 0; i < petSexComboBox.Items.Count; i++)
                {
                    if (petSexComboBox.Items[i].ToString() == pet.Sex)
                    {
                        petSexComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            // TODO: Установить выбранные вид и породу (после загрузки данных)
            // Нужно дождаться загрузки видов и пород, затем установить выбранные

            // Устанавливаем дату рождения
            if (!string.IsNullOrEmpty(pet.Birthday) &&
                DateTime.TryParse(pet.Birthday, out DateTime birthday))
            {
                petDayOfBirthPicker.Value = birthday;
            }
        }

        private async void SaveBtn_Click(object? sender, EventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(petAliasTextBox.Text))
            {
                MessageBox.Show("Введите кличку питомца!", "Ошибка");
                petAliasTextBox.Focus();
                return;
            }

            try
            {
                saveBtn.Enabled = false;

                if (_isEditMode && EditedPet != null)
                {
                    // Режим редактирования
                    await EditPetAsync();
                    MessageBox.Show("Питомец успешно обновлен!", "Успех");
                }
                else
                {
                    // Режим добавления
                    await AddPetAsync();
                    MessageBox.Show("Питомец успешно добавлен!", "Успех");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
            finally
            {
                saveBtn.Enabled = true;
            }
        }

        private async Task AddPetAsync()
        {
            // TODO: Реализовать вызов API для создания питомца
            throw new NotImplementedException("Метод добавления питомца еще не реализован");
        }

        private async Task EditPetAsync()
        {
            // TODO: Реализовать вызов API для обновления питомца
            throw new NotImplementedException("Метод редактирования питомца еще не реализован");
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}