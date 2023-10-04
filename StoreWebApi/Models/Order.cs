using StoreWebApi.EnumsObjects;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Client? Client { get; set; }
        public int ClientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OpenedIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime ClosedOut { get; set; }
        public Freight Freight { get; set; }
        public OrderStatus OrderStatus {get; set;}
        public string? Comments { get; set;}
        public ICollection<OrderItem> Items { get; set; }
    }
}
