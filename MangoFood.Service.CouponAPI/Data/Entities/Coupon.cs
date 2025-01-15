namespace MangoFood.Service.CouponAPI.Data.Entities
{
    public class Coupon
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
