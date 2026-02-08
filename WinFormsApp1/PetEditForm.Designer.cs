namespace WinFormsApp1
{
    partial class PetEditForm
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
            petAlias = new Label();
            petTypeLabel = new Label();
            petBreedLabel = new Label();
            petSexLabel = new Label();
            petBirthdayLabel = new Label();
            saveBtn = new Button();
            cancelBtn = new Button();
            petAliasTextBox = new TextBox();
            petTypeComboBox = new ComboBox();
            petBreedComboBox = new ComboBox();
            petSexComboBox = new ComboBox();
            petDayOfBirthPicker = new DateTimePicker();
            SuspendLayout();
            // 
            // petAlias
            // 
            petAlias.AutoSize = true;
            petAlias.Font = new Font("Segoe UI", 10F);
            petAlias.Location = new Point(12, 9);
            petAlias.Name = "petAlias";
            petAlias.Size = new Size(67, 19);
            petAlias.TabIndex = 0;
            petAlias.Text = "Кличка: *";
            // 
            // petTypeLabel
            // 
            petTypeLabel.AutoSize = true;
            petTypeLabel.Font = new Font("Segoe UI", 10F);
            petTypeLabel.Location = new Point(12, 48);
            petTypeLabel.Name = "petTypeLabel";
            petTypeLabel.Size = new Size(36, 19);
            petTypeLabel.TabIndex = 1;
            petTypeLabel.Text = "Вид:";
            // 
            // petBreedLabel
            // 
            petBreedLabel.AutoSize = true;
            petBreedLabel.Font = new Font("Segoe UI", 10F);
            petBreedLabel.Location = new Point(12, 95);
            petBreedLabel.Name = "petBreedLabel";
            petBreedLabel.Size = new Size(61, 19);
            petBreedLabel.TabIndex = 2;
            petBreedLabel.Text = "Порода:";
            // 
            // petSexLabel
            // 
            petSexLabel.AutoSize = true;
            petSexLabel.Font = new Font("Segoe UI", 10F);
            petSexLabel.Location = new Point(12, 145);
            petSexLabel.Name = "petSexLabel";
            petSexLabel.Size = new Size(37, 19);
            petSexLabel.TabIndex = 3;
            petSexLabel.Text = "Пол:";
            // 
            // petBirthdayLabel
            // 
            petBirthdayLabel.AutoSize = true;
            petBirthdayLabel.Font = new Font("Segoe UI", 10F);
            petBirthdayLabel.Location = new Point(12, 193);
            petBirthdayLabel.Name = "petBirthdayLabel";
            petBirthdayLabel.Size = new Size(80, 19);
            petBirthdayLabel.TabIndex = 4;
            petBirthdayLabel.Text = "Дата рожд:";
            // 
            // saveBtn
            // 
            saveBtn.Font = new Font("Segoe UI", 10F);
            saveBtn.Location = new Point(12, 334);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(131, 35);
            saveBtn.TabIndex = 5;
            saveBtn.Text = "Сохранить";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.Font = new Font("Segoe UI", 10F);
            cancelBtn.Location = new Point(172, 334);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(135, 35);
            cancelBtn.TabIndex = 6;
            cancelBtn.Text = "Отмена";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // petAliasTextBox
            //
            petAliasTextBox.BorderStyle = BorderStyle.FixedSingle;
            petAliasTextBox.Location = new Point(111, 8);
            petAliasTextBox.Name = "petAliasTextBox";
            petAliasTextBox.Size = new Size(200, 23);
            petAliasTextBox.TabIndex = 7;
            // 
            // petTypeComboBox
            // 
            petTypeComboBox.FormattingEnabled = true;
            petTypeComboBox.Location = new Point(111, 47);
            petTypeComboBox.Name = "petTypeComboBox";
            petTypeComboBox.Size = new Size(200, 23);
            petTypeComboBox.TabIndex = 8;
            // 
            // petBreedComboBox
            // 
            petBreedComboBox.FormattingEnabled = true;
            petBreedComboBox.Location = new Point(111, 94);
            petBreedComboBox.Name = "petBreedComboBox";
            petBreedComboBox.Size = new Size(200, 23);
            petBreedComboBox.TabIndex = 9;
            // 
            // petSexComboBox
            // 
            petSexComboBox.FormattingEnabled = true;
            petSexComboBox.Location = new Point(111, 145);
            petSexComboBox.Name = "petSexComboBox";
            petSexComboBox.Size = new Size(200, 23);
            petSexComboBox.TabIndex = 10;
            // 
            // petDayOfBirthPicker
            // 
            petDayOfBirthPicker.Font = new Font("Segoe UI", 10F);
            petDayOfBirthPicker.Location = new Point(111, 187);
            petDayOfBirthPicker.Name = "petDayOfBirthPicker";
            petDayOfBirthPicker.Size = new Size(200, 25);
            petDayOfBirthPicker.TabIndex = 11;
            // 
            // PetEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 380);
            Controls.Add(petDayOfBirthPicker);
            Controls.Add(petSexComboBox);
            Controls.Add(petBreedComboBox);
            Controls.Add(petTypeComboBox);
            Controls.Add(petAliasTextBox);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(petBirthdayLabel);
            Controls.Add(petSexLabel);
            Controls.Add(petBreedLabel);
            Controls.Add(petTypeLabel);
            Controls.Add(petAlias);
            Name = "PetEditForm";
            Text = "Добавление питомца";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label petAlias;
        private Label petTypeLabel;
        private Label petBreedLabel;
        private Label petSexLabel;
        private Label petBirthdayLabel;
        private Button saveBtn;
        private Button cancelBtn;
        private TextBox petAliasTextBox;
        private ComboBox petTypeComboBox;
        private ComboBox petBreedComboBox;
        private ComboBox petSexComboBox;
        private DateTimePicker petDayOfBirthPicker;
    }
}