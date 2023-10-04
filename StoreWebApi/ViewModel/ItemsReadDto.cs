namespace StoreWebApi.ViewModel
{
    public class ItemsReadDto
    {
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
