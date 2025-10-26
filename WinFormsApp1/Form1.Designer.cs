namespace WinFormsApp1
{
    partial class mainForm
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
            APISettings = new Button();
            AddBtn = new Button();
            EditBtn = new Button();
            DeleteBtn = new Button();
            SuspendLayout();
            // 
            // APISettings
            // 
            APISettings.Font = new Font("Segoe UI", 10F);
            APISettings.Location = new Point(613, 14);
            APISettings.Name = "APISettings";
            APISettings.Size = new Size(175, 35);
            APISettings.TabIndex = 0;
            APISettings.Text = "Настройки API";
            APISettings.UseVisualStyleBackColor = true;
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
            EditBtn.Click += EditBtn_Click;
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
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(DeleteBtn);
            Controls.Add(EditBtn);
            Controls.Add(AddBtn);
            Controls.Add(APISettings);
            Font = new Font("Segoe UI", 10F);
            Name = "mainForm";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button APISettings;
        private Button AddBtn;
        private Button EditBtn;
        private Button DeleteBtn;
    }
}
