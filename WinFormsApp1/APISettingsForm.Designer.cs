namespace WinFormsApp1
{
    partial class APISettingsForm
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
            label1 = new Label();
            domainTextBox = new TextBox();
            label2 = new Label();
            loginTextBox = new TextBox();
            label3 = new Label();
            passwordTextBox = new TextBox();
            ConnectBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(12, 34);
            label1.Name = "label1";
            label1.Size = new Size(55, 19);
            label1.TabIndex = 0;
            label1.Text = "Домен:";
            // 
            // domainTextBox
            // 
            domainTextBox.BorderStyle = BorderStyle.FixedSingle;
            domainTextBox.Location = new Point(77, 32);
            domainTextBox.Name = "domainTextBox";
            domainTextBox.Size = new Size(328, 25);
            domainTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(12, 85);
            label2.Name = "label2";
            label2.Size = new Size(50, 19);
            label2.TabIndex = 2;
            label2.Text = "Логин:";
            // 
            // loginTextBox
            // 
            loginTextBox.BorderStyle = BorderStyle.FixedSingle;
            loginTextBox.Font = new Font("Segoe UI", 10F);
            loginTextBox.Location = new Point(77, 83);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.Size = new Size(328, 25);
            loginTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(12, 133);
            label3.Name = "label3";
            label3.Size = new Size(59, 19);
            label3.TabIndex = 4;
            label3.Text = "Пароль:";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Location = new Point(77, 130);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(328, 25);
            passwordTextBox.TabIndex = 5;
            // 
            // ConnectBtn
            // 
            ConnectBtn.Location = new Point(144, 175);
            ConnectBtn.Name = "ConnectBtn";
            ConnectBtn.Size = new Size(157, 34);
            ConnectBtn.TabIndex = 6;
            ConnectBtn.Text = "Связать";
            ConnectBtn.UseVisualStyleBackColor = true;
            // 
            // APISettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(563, 219);
            Controls.Add(ConnectBtn);
            Controls.Add(passwordTextBox);
            Controls.Add(label3);
            Controls.Add(loginTextBox);
            Controls.Add(label2);
            Controls.Add(domainTextBox);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10F);
            Name = "APISettingsForm";
            Text = "Настройки API";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox domainTextBox;
        private Label label2;
        private TextBox loginTextBox;
        private Label label3;
        private TextBox passwordTextBox;
        private Button ConnectBtn;
    }
}