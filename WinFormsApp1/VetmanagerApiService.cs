using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                using var client = CreateHttpClient();
                var url = $"https://{_domain}.vetmanager2.ru/rest/api/pet?client_id={clientId}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return Array.Empty<Pet>();
                }

                var json = await response.Content.ReadAsStringAsync();

                using JsonDocument document = JsonDocument.Parse(json);

                if (document.RootElement.TryGetProperty("success", out var successElement) &&
                    successElement.GetBoolean() &&
                    document.RootElement.TryGetProperty("data", out var dataElement) &&
                    dataElement.TryGetProperty("pet", out var petElement))
                {
                    var pets = new List<Pet>();

                    foreach (var petJson in petElement.EnumerateArray())
                    {
                        var pet = new Pet();

                        if (petJson.TryGetProperty("id", out var idElement))
                            pet.Id = idElement.GetInt32();

                        if (petJson.TryGetProperty("alias", out var aliasElement))
                            pet.Alias = aliasElement.GetString() ?? "";

                        if (petJson.TryGetProperty("owner_id", out var ownerIdElement))
                            pet.OwnerId = ownerIdElement.GetInt32();

                        if (petJson.TryGetProperty("sex", out var sexElement))
                            pet.Sex = sexElement.GetString();

                        if (petJson.TryGetProperty("type_id", out var typeIdElement) &&
                            typeIdElement.ValueKind != JsonValueKind.Null)
                        {
                            pet.TypeId = typeIdElement.GetInt32();
                        }

                        if (petJson.TryGetProperty("breed_id", out var breedIdElement) &&
                            breedIdElement.ValueKind != JsonValueKind.Null)
                        {
                            pet.BreedId = breedIdElement.GetInt32();
                        }

                        if (pet.OwnerId == clientId)
                        {
                            pets.Add(pet);
                        }
                    }

                    return pets.ToArray();
                }

                return Array.Empty<Pet>();
            }
            catch (Exception)
            {
                return Array.Empty<Pet>();
            }
        }

        private Pet[] ParsePetsResponse(string json)
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(json);

                // Проверяем success
                if (document.RootElement.TryGetProperty("success", out var successElement))
                {
                    if (!successElement.GetBoolean())
                    {
                        Debug.WriteLine("API вернул success: false");
                        return Array.Empty<Pet>();
                    }
                }

                // Ищем data
                if (document.RootElement.TryGetProperty("data", out var dataElement))
                {
                    // Если data - массив
                    if (dataElement.ValueKind == JsonValueKind.Array)
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var pets = JsonSerializer.Deserialize<Pet[]>(dataElement.GetRawText(), options);
                        return pets ?? Array.Empty<Pet>();
                    }
                    // Если data - объект с pet/pets
                    else if (dataElement.ValueKind == JsonValueKind.Object)
                    {
                        if (dataElement.TryGetProperty("pet", out var petElement))
                        {
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            var pets = JsonSerializer.Deserialize<Pet[]>(petElement.GetRawText(), options);
                            return pets ?? Array.Empty<Pet>();
                        }
                        else if (dataElement.TryGetProperty("pets", out var petsElement))
                        {
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            var pets = JsonSerializer.Deserialize<Pet[]>(petsElement.GetRawText(), options);
                            return pets ?? Array.Empty<Pet>();
                        }
                    }
                }

                Debug.WriteLine($"Неизвестный формат JSON: {json}");
                return Array.Empty<Pet>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка парсинга JSON: {ex.Message}");
                return Array.Empty<Pet>();
            }
        }
    }
}
