using System.ComponentModel.DataAnnotations;

namespace MangoFood.Service.OrderAPI.Models.DTOs
{
    public class OrderDto
    {
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
