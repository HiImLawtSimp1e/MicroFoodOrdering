namespace MangoFood.Service.ShoppingCartAPI.Data.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
