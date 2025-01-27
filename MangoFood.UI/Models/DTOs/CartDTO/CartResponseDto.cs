namespace MangoFood.UI.Models.DTOs.CartDTO
{
    public class CartResponseDto
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalAmount { get; set; }
        public List<CartItemResponseDto>? CartItems { get; set; }
    }
}
