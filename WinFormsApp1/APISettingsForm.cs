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

                    if (serializer.Deserialize(reader) is ApiSettings settings)
                    {
                        domainTextBox.Text = settings.Domain;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки настроек: {ex.Message}", "Ошибка");
                }
            }
        }

        private async void ConnectBtn_Click(object? sender, EventArgs e)
        {
            string domain = domainTextBox.Text.Trim();
            string login = loginTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

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
                ConnectBtn.Enabled = false;
                ConnectBtn.Text = "Подключение...";

                var tempApiService = new VetmanagerApiService("", "");
                string token = await tempApiService.GetTokenAsync(domain, login, password);

                var settings = new ApiSettings
                {
                    Domain = domain,
                    Token = token,
                };

                XmlSerializer serializer = new XmlSerializer(typeof(ApiSettings));
                using var writer = new StreamWriter("settings.xml");
                serializer.Serialize(writer, settings);

                MessageBox.Show("Подключение успешно установлено!", "Успешно");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка");
            }
            finally
            {
                ConnectBtn.Enabled = true;
                ConnectBtn.Text = "Связать";
            }
        }
    }
}
