using MangoFood.UI.Models.DTOs.ProductDTO;

namespace MangoFood.UI.Models.DTOs.OrderDTO
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public double Price { get; set; }
    }
}
