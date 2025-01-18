using MangoFood.Service.AuthAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MangoFood.Service.AuthAPI.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}

