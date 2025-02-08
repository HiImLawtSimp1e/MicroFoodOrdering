namespace MangoFood.UI.Models.DTOs.OrderDTO
{
    public class OrderItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
