using StoreWebApi.EnumsObjects;

namespace StoreWebApi.ViewModel;

public class CreateOrderItems
{
    public int ClientId { get; set; }
    public Freight Freight { get; set; }
    public string? Comments { get; set; }
    public ICollection<ItemsCreateDto> Items { get; set; }
}