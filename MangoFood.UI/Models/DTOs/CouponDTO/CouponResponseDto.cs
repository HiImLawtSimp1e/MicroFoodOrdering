namespace MangoFood.UI.Models.DTOs.CouponDTO
{
    public class CouponResponseDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
