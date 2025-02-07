using System.ComponentModel.DataAnnotations;

namespace MangoFood.Service.ShoppingCartAPI.Models.DTOs
{
    public class OrderDto
    {
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
