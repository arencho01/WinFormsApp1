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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(DeleteBtn);
            Controls.Add(EditBtn);
            Controls.Add(AddBtn);
            Controls.Add(APISettingsBtn);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button APISettingsBtn;
        private Button AddBtn;
        private Button EditBtn;
        private Button DeleteBtn;
    }
}
