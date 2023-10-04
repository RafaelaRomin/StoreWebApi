using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Phone { get; set; } 
        public string? ZipCode { get; set; } 
        public string? State { get; set; }
        public string? City { get; set; }
    }
}
