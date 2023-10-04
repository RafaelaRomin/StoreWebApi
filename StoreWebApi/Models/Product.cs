using StoreWebApi.EnumsObjects;

namespace StoreWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? BarCode { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public bool Active { get; set; }
    }
}
