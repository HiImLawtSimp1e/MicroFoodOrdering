namespace MangoFood.UI.Models.DTOs.OrderDTO
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public List<OrderItemResponseDto>? OrderItems { get; set; }
    }
}
