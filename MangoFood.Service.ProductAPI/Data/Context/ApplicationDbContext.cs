using MangoFood.Service.ProductAPI.Data.Entities;
using MangoFood.Service.ProductAPI.Data.Initialization;

namespace MangoFood.Service.ProductAPI.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seeding.SeedingData(modelBuilder);
        }
    }
}
