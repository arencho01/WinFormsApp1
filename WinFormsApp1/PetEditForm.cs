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
        private int? _selectedTypeId;
        private int? _selectedBreedId;

        public PetEditForm(VetmanagerApiService apiService, int ownerId, Pet? petToEdit = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _apiService = apiService;
            _ownerId = ownerId;

            // Настраиваем SexComboBox
            petSexComboBox.Items.AddRange(new object[] { "male", "female", "castrated", "sterilized", "unknown" });
            petSexComboBox.SelectedIndex = 0;

            if (petToEdit != null)
            {
                _isEditMode = true;
                EditedPet = petToEdit;
                this.Text = "Редактирование питомца";

                // Просто сохраняем данные, НЕ загружаем в форму сразу
                _selectedTypeId = petToEdit.TypeId;
                _selectedBreedId = petToEdit.BreedId;
            }
            else
            {
                this.Text = "Добавление питомца";
            }

            saveBtn.Click += SaveBtn_Click;
            cancelBtn.Click += CancelBtn_Click;
            petTypeComboBox.SelectedIndexChanged += PetTypeComboBox_SelectedIndexChanged;

            // Подписываемся на событие Load
            this.Load += PetEditForm_Load;
        }

        private async void PetEditForm_Load(object? sender, EventArgs e)
        {
            try
            {
                // Показываем индикатор загрузки
                SetLoadingState(true);

                // Загружаем основные данные питомца (кличка, пол, дата)
                if (EditedPet != null)
                {
                    petAliasTextBox.Text = EditedPet.Alias;

                    // Устанавливаем пол
                    if (!string.IsNullOrEmpty(EditedPet.Sex))
                    {
                        for (int i = 0; i < petSexComboBox.Items.Count; i++)
                        {
                            if (petSexComboBox.Items[i].ToString() == EditedPet.Sex)
                            {
                                petSexComboBox.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    // Устанавливаем дату рождения
                    if (!string.IsNullOrEmpty(EditedPet.Birthday) &&
                        DateTime.TryParse(EditedPet.Birthday, out DateTime birthday))
                    {
                        petDayOfBirthPicker.Value = birthday;
                    }
                }

                // Загружаем виды питомцев
                await LoadPetTypesAsync();

                // Если это режим редактирования, устанавливаем правильный вид и породу
                if (_isEditMode)
                {
                    await SetSelectedPetTypeAndBreedAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка");
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            if (isLoading)
            {
                Cursor = Cursors.WaitCursor;
                petTypeComboBox.Enabled = false;
                petBreedComboBox.Enabled = false;

                // Сохраняем текущие элементы, если они есть
                if (petTypeComboBox.Items.Count == 0 || petTypeComboBox.Items[0].ToString() != "Загрузка...")
                {
                    petTypeComboBox.Items.Clear();
                    petTypeComboBox.Items.Add("Загрузка...");
                    petTypeComboBox.SelectedIndex = 0;
                }

                if (petBreedComboBox.Items.Count == 0 || petBreedComboBox.Items[0].ToString() != "Загрузка...")
                {
                    petBreedComboBox.Items.Clear();
                    petBreedComboBox.Items.Add("Загрузка...");
                    petBreedComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                Cursor = Cursors.Default;
                petTypeComboBox.Enabled = true;
                petBreedComboBox.Enabled = true;

                // НЕ очищаем комбобоксы здесь!
                // Они должны уже содержать реальные данные из LoadPetTypesAsync
            }
        }

        private async Task SetSelectedPetTypeAndBreedAsync()
        {
            // Убеждаемся, что виды загружены
            if (petTypeComboBox.Items.Count == 0 ||
                (petTypeComboBox.Items.Count == 1 && petTypeComboBox.Items[0].ToString() == "Загрузка..."))
            {
                // Ждем загрузки видов
                await Task.Delay(100);
            }

            // Устанавливаем выбранный вид
            if (_selectedTypeId.HasValue)
            {
                bool typeFound = false;

                for (int i = 0; i < petTypeComboBox.Items.Count; i++)
                {
                    if (petTypeComboBox.Items[i] is PetType petType && petType.Id == _selectedTypeId.Value)
                    {
                        petTypeComboBox.SelectedIndex = i;
                        typeFound = true;
                        break;
                    }
                }

                if (!typeFound && petTypeComboBox.Items.Count > 0)
                {
                    // Если вид не найден, выбираем первый настоящий элемент (не "Загрузка...")
                    for (int i = 0; i < petTypeComboBox.Items.Count; i++)
                    {
                        if (petTypeComboBox.Items[i] is PetType)
                        {
                            petTypeComboBox.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Ждем загрузки пород для выбранного вида
                await LoadBreedsForSelectedTypeAsync();

                // Устанавливаем выбранную породу
                if (_selectedBreedId.HasValue && petBreedComboBox.Items.Count > 0)
                {
                    bool breedFound = false;

                    for (int j = 0; j < petBreedComboBox.Items.Count; j++)
                    {
                        if (petBreedComboBox.Items[j] is PetBreed breed && breed.Id == _selectedBreedId.Value)
                        {
                            petBreedComboBox.SelectedIndex = j;
                            breedFound = true;
                            break;
                        }
                    }

                    if (!breedFound && petBreedComboBox.Items.Count > 0)
                    {
                        // Если порода не найдена, выбираем первый настоящий элемент
                        for (int j = 0; j < petBreedComboBox.Items.Count; j++)
                        {
                            if (petBreedComboBox.Items[j] is PetBreed)
                            {
                                petBreedComboBox.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private async Task LoadPetTypesAsync()
        {
            try
            {
                var petTypes = await _apiService.GetPetTypesAsync();

                petTypeComboBox.BeginUpdate();
                petTypeComboBox.Items.Clear();

                foreach (var petType in petTypes)
                {
                    petTypeComboBox.Items.Add(petType);
                }

                petTypeComboBox.EndUpdate();

                // Если это режим редактирования, НЕ выбираем первый элемент автоматически
                // Мы выберем правильный вид позже в SetSelectedPetTypeAndBreedAsync
                if (!_isEditMode && petTypeComboBox.Items.Count > 0)
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

                    // Очищаем комбобокс и показываем "Загрузка..."
                    petBreedComboBox.BeginUpdate();
                    petBreedComboBox.Items.Clear();
                    petBreedComboBox.Items.Add("Загрузка...");
                    petBreedComboBox.SelectedIndex = 0;
                    petBreedComboBox.EndUpdate();

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
                for (int i = 0; i < petSexComboBox.Items.Count; i++)
                {
                    if (petSexComboBox.Items[i].ToString() == pet.Sex)
                    {
                        petSexComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Устанавливаем дату рождения
            if (!string.IsNullOrEmpty(pet.Birthday) &&
                DateTime.TryParse(pet.Birthday, out DateTime birthday))
            {
                petDayOfBirthPicker.Value = birthday;
            }
            else
            {
                petDayOfBirthPicker.Value = DateTime.Today;
            }

            // Запомним ID вида и породы для установки после загрузки данных
            _selectedTypeId = pet.TypeId;
            _selectedBreedId = pet.BreedId;
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

                // ЭТА СТРОКА УСТАНАВЛИВАЕТ DialogResult.OK
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
            // Создаем нового питомца
            var pet = new Pet
            {
                Alias = petAliasTextBox.Text.Trim(),
                OwnerId = _ownerId,
                Sex = petSexComboBox.SelectedItem?.ToString()
            };

            // Добавляем выбранный вид, если есть
            if (petTypeComboBox.SelectedItem is PetType selectedType)
            {
                pet.TypeId = selectedType.Id;
            }

            // Добавляем выбранную породу, если есть
            if (petBreedComboBox.SelectedItem is PetBreed selectedBreed)
            {
                pet.BreedId = selectedBreed.Id;
            }

            // Добавляем дату рождения
            pet.Birthday = petDayOfBirthPicker.Value.ToString("yyyy-MM-dd");

            try
            {
                // Вызываем API для создания питомца
                var createdPet = await _apiService.CreatePetAsync(pet);

                // Можно вывести информацию о созданном питомце
                Debug.WriteLine($"Питомец создан с ID: {createdPet.Id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось создать питомца: {ex.Message}");
            }
        }

        private async Task EditPetAsync()
        {
            if (EditedPet == null) return;

            // Обновляем данные питомца из формы
            EditedPet.Alias = petAliasTextBox.Text.Trim();
            EditedPet.Sex = petSexComboBox.SelectedItem?.ToString();

            // Обновляем выбранный вид, если есть
            if (petTypeComboBox.SelectedItem is PetType selectedType)
            {
                EditedPet.TypeId = selectedType.Id;
            }

            // Обновляем выбранную породу, если есть
            if (petBreedComboBox.SelectedItem is PetBreed selectedBreed)
            {
                EditedPet.BreedId = selectedBreed.Id;
            }

            // Обновляем дату рождения
            EditedPet.Birthday = petDayOfBirthPicker.Value.ToString("yyyy-MM-dd");

            try
            {
                // Вызываем API для обновления питомца
                var updatedPet = await _apiService.UpdatePetAsync(EditedPet);

                // Можно вывести информацию об обновленном питомце
                Debug.WriteLine($"Питомец обновлен с ID: {updatedPet.Id}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось обновить питомца: {ex.Message}");
            }
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}