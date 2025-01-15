using MangoFood.Service.CouponAPI.Data.Entities;

namespace MangoFood.Service.CouponAPI.Data.Initialization
{
    public class Seeding
    {
        public static void SeedingData(ModelBuilder builder)
        {
            builder.Entity<Coupon>().HasData(new Coupon
            {
                Id = Guid.NewGuid(),
                CouponCode = "10OFF",
                DiscountAmount = 10,
                MinAmount = 20
            });

            builder.Entity<Coupon>().HasData(new Coupon
            {
                Id = Guid.NewGuid(),
                CouponCode = "20OFF",
                DiscountAmount = 20,
                MinAmount = 40
            });
        }
    }
}
