using System.ComponentModel.DataAnnotations;

namespace Mango.Service.OrderAPI.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; } 
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
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }

    }
}
