using MangoFood.Service.CouponAPI.Data.Entities;
using MangoFood.Service.CouponAPI.Data.Initialization;

namespace MangoFood.Service.CouponAPI.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seeding.SeedingData(modelBuilder);
        }
    }
}
