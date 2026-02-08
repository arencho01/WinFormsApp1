using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            // TODO: Загрузить виды и породы из API
            // TODO: Установить выбранные вид и породу

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