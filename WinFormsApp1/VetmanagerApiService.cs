using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                var url = $"https://{domain}.vetmanager2.ru/token_auth.php";
                var response = await authClient.PostAsync(url, formData);

                string responseJson = await response.Content.ReadAsStringAsync();

                using JsonDocument document = JsonDocument.Parse(responseJson);

                // Проверяем статус
                if (document.RootElement.TryGetProperty("status", out var statusElement))
                {
                    int status = statusElement.GetInt32();
                    if (status != 200)
                    {
                        string errorMessage = "Ошибка авторизации";
                        if (document.RootElement.TryGetProperty("detail", out var detailElement))
                        {
                            errorMessage = detailElement.GetString() ?? errorMessage;
                        }
                        throw new Exception(errorMessage);
                    }
                }

                // Получаем токен из data.token
                if (document.RootElement.TryGetProperty("data", out var dataElement))
                {
                    if (dataElement.TryGetProperty("token", out var tokenElement))
                    {
                        var token = tokenElement.GetString();
                        return token ?? throw new Exception("Токен пустой");
                    }
                }

                throw new Exception("Не удалось найти токен в ответе");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка получения токена: {ex.Message}");
            }
        }

        public async Task<Client[]> GetClientsAsync()
        {
            using var client = CreateHttpClient();
            var url = $"https://{_domain}.vetmanager2.ru/rest/api/client";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"HTTP ошибка: {response.StatusCode}\n{errorContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ClientApiResponse>(json);
            return apiResponse?.Data?.Clients ?? Array.Empty<Client>();
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
