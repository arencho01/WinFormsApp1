using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinFormsApp1
{
    // 1. Общая структура для большинства ответов
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public T[] Data { get; set; } = Array.Empty<T>();
    }

    // 2. Специальная структура ТОЛЬКО для ответа с клиентами
    public class ClientApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public ClientApiData? Data { get; set; }
    }

    public class ClientApiData
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("client")]
        public Client[] Clients { get; set; } = Array.Empty<Client>();
    }

    public class ApiSettings
    {
        public string Domain { get; set; } = "";
        public string Token { get; set; } = "";
    }

    public class Client
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = "";

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = "";

        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; } = "";

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}".Trim();
        }
    }

    public class Pet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("alias")]
        public string Alias { get; set; } = "";

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        [JsonPropertyName("breed_id")]
        public int BreedId { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; } = "";

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
    }
}
