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
            client.DefaultRequestHeaders.Add("X-APP-NAME", "!!!!!!!!------same_name_as_in_get_token_request");

            return client;
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

        public async Task<string> GetTokenAsync(string domain, string login, string password)
        {
            await Task.Delay(100);

            return "real token wil be here soon";
        }
    }
}
