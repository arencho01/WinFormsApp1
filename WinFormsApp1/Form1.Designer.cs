namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            APISettingsBtn = new Button();
            AddBtn = new Button();
            EditBtn = new Button();
            DeleteBtn = new Button();
            label1 = new Label();
            ClientComboBox = new ComboBox();
            label2 = new Label();
            PetsDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)PetsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // APISettingsBtn
            // 
            APISettingsBtn.Font = new Font("Segoe UI", 10F);
            APISettingsBtn.Location = new Point(613, 14);
            APISettingsBtn.Name = "APISettingsBtn";
            APISettingsBtn.Size = new Size(175, 35);
            APISettingsBtn.TabIndex = 0;
            APISettingsBtn.Text = "Настройки API";
            APISettingsBtn.UseVisualStyleBackColor = true;
            // 
            // AddBtn
            // 
            AddBtn.Location = new Point(258, 68);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(100, 35);
            AddBtn.TabIndex = 1;
            AddBtn.Text = "Добавить";
            AddBtn.UseVisualStyleBackColor = true;
            // 
            // EditBtn
            // 
            EditBtn.Location = new Point(380, 68);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(150, 35);
            EditBtn.TabIndex = 2;
            EditBtn.Text = "Редактировать";
            EditBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            DeleteBtn.Location = new Point(552, 68);
            DeleteBtn.Name = "DeleteBtn";
            DeleteBtn.Size = new Size(107, 35);
            DeleteBtn.TabIndex = 3;
            DeleteBtn.Text = "Удалить";
            DeleteBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(56, 19);
            label1.TabIndex = 4;
            label1.Text = "Клиент:";
            // 
            // ClientComboBox
            // 
            ClientComboBox.FormattingEnabled = true;
            ClientComboBox.Location = new Point(74, 9);
            ClientComboBox.Name = "ClientComboBox";
            ClientComboBox.Size = new Size(121, 25);
            ClientComboBox.TabIndex = 5;
            ClientComboBox.SelectedIndexChanged += ClientСomboBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 77);
            label2.Name = "label2";
            label2.Size = new Size(126, 19);
            label2.TabIndex = 6;
            label2.Text = "Питомцы клиента:";
            // 
            // PetsDataGridView
            // 
            PetsDataGridView.AllowUserToAddRows = false;
            PetsDataGridView.AllowUserToDeleteRows = false;
            PetsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PetsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PetsDataGridView.Location = new Point(12, 109);
            PetsDataGridView.Name = "PetsDataGridView";
            PetsDataGridView.ReadOnly = true;
            PetsDataGridView.RowHeadersVisible = false;
            PetsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            PetsDataGridView.Size = new Size(776, 389);
            PetsDataGridView.TabIndex = 7;
            PetsDataGridView.SelectionChanged += PetsDataGridView_SelectionChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(PetsDataGridView);
            Controls.Add(label2);
            Controls.Add(ClientComboBox);
            Controls.Add(label1);
            Controls.Add(DeleteBtn);
            Controls.Add(EditBtn);
            Controls.Add(AddBtn);
            Controls.Add(APISettingsBtn);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)PetsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button APISettingsBtn;
        private Button AddBtn;
        private Button EditBtn;
        private Button DeleteBtn;
        private Label label1;
        private ComboBox ClientComboBox;
        private Label label2;
        private DataGridView PetsDataGridView;
    }
}
