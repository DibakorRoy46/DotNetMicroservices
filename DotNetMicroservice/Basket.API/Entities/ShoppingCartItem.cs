namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductId { get; set; }=string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
