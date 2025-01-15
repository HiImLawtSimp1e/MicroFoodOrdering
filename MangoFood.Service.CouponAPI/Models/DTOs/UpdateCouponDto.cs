namespace MangoFood.Service.CouponAPI.Models.DTOs
{
    public class UpdateCouponDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
