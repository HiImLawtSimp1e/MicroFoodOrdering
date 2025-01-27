namespace MangoFood.Service.ShoppingCartAPI.Models.DTOs
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
