using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinFormsApp1
{
    public class ApiResponse<T>
    {
        public string? Status { get; set; }
        public T[] Data { get; set } = Array.Empty<T>();
    }
    public class ApiSettings
    {
        public string Domain { get; set; } = "";
        public string Token { get; set; } = "";
    }

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }

    public class Pet
    {
        public int Id { get; set; }
        public string Alias { get; set; } = "";
        public int TypeId { get; set; }
        public int BreedId { get; set; }
        public string Sex { get; set; } = "";
    }
}
