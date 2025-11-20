using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class APISettingsForm : Form
    {
        public APISettingsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ConnectBtn.Click += ConnectBtn_Click;


            LoadExistingSettings();
        }

        private void LoadExistingSettings()
        {
            if (File.Exists("settings.xml"))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ApiSettings));

                    using var reader = new StreamReader("settings.xml");

                    var result = serializer.Deserialize(reader);

                    if (result is ApiSettings settings)
                    {
                        domainTextBox.Text = settings.Domain;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: неверный формат файла настроек");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки настроек: {ex.Message}", "Ошибка");
                    throw;
                }
            }
        }

        private void ConnectBtn_Click (object? sender, EventArgs e)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            MessageBox.Show($"Файл будет сохранен в: {currentDirectory}", "Информация");

            string domain = this.domainTextBox.Text.Trim();
            string login = this.loginTextBox.Text.Trim();
            string password = this.passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(domain))
            {
                MessageBox.Show("Введите домен!", "Ошибка");
                domainTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Введите логин!", "Ошибка");
                loginTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка");
                passwordTextBox.Focus();
                return;
            }

            try
            {
                var settings = new ApiSettings
                {
                    Domain = domain,
                    Token = "temp_token",
                };

                XmlSerializer serializer = new XmlSerializer(typeof(ApiSettings));
                using var writer = new StreamWriter("settings.xml");
                serializer.Serialize(writer, settings);

                MessageBox.Show("Настройки сохранены! (токен временный)", "Успешно");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
                throw;
            }
        }
    }
}
