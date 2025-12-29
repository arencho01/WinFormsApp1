using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class VetmanagerApiService
    {
        private readonly string _domain;
        private readonly string _token;

        public VetmanagerApiService(string domain, string token)
        {
            _domain = domain;
            _token = token;
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-USER-TOKEN", _token);
            client.DefaultRequestHeaders.Add("X-APP-NAME", "MyApp");

            return client;
        }

        public async Task<string> GetTokenAsync(string domain, string login, string password)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(login), "login");
                formData.Add(new StringContent(password), "password");
                formData.Add(new StringContent("MyApp"), "app_name");

                var authClient = new HttpClient();
                var response = await authClient.PostAsync(
                    $"https://{domain}.vetmanager.ru/token_auth.php",
                    formData
                );

                response.EnsureSuccessStatusCode();

                string responseJson = await response.Content.ReadAsStringAsync();

                using JsonDocument document = JsonDocument.Parse(responseJson);
                var token = document.RootElement
                    .GetProperty("token")
                    .GetString();

                return token ?? throw new Exception("Токен не получен");

            }
            catch (Exception ex)
            {

                throw new Exception($"Ошибка получения токена: {ex.Message}");
            }
        }

        public async Task<Client[]> GetClientsAsync()
        {
            using var client = CreateHttpClient();
            var url = $"https://{_domain}.vetmanager.ru/rest/api/clients";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<Client>>(json);

            return apiResponse?.Data ?? Array.Empty<Client>();
        }

        public async Task<Pet[]> GetPetsByClientIdAsync(int clientId)
        {
            using var client = CreateHttpClient();
            var url = $"https://{_domain}.vetmanager2.ru/rest/api/pets?client_id={clientId}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<Pet>>(json);

            return apiResponse?.Data ?? Array.Empty<Pet>();
        }
    }
}
