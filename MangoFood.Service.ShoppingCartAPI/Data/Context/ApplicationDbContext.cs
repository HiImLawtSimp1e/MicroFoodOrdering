using MangoFood.Service.ShoppingCartAPI.Data.Entities;

namespace MangoFood.Service.ShoppingCartAPI.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
