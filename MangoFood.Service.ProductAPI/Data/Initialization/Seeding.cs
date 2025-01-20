using MangoFood.Service.ProductAPI.Data.Entities;

namespace MangoFood.Service.ProductAPI.Data.Initialization
{
    public class Seeding
    {
        public static void SeedingData(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
            new Product
            {
               Id = Guid.NewGuid(),
               Name = "Pizza Margherita",
               Price = 9.99,
               Description = "Classic Italian pizza with fresh mozzarella and basil.",
               CategoryName = "Pizza",
               ImageUrl = "https://i.pinimg.com/736x/d4/bf/d5/d4bfd54c3f5f672af5be24513e12902f.jpg"
            }, 
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Spaghetti Carbonara",
                Price = 12.99,
                Description = "Traditional pasta with creamy egg sauce, pancetta, and Parmesan.",
                CategoryName = "Pasta",
                ImageUrl = "https://i.pinimg.com/736x/32/98/80/329880050b7fded253ca69bd31cd84d8.jpg"
            }, 
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Caesar Salad",
                Price = 7.49,
                Description = "Fresh romaine lettuce, croutons, Parmesan cheese, and Caesar dressing.",
                CategoryName = "Salad",
                ImageUrl = "https://i.pinimg.com/736x/68/eb/9e/68eb9eac17e7649a84ab3b485e01a843.jpg"
            }, 
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cheeseburger",
                Price = 8.99,
                Description = "Juicy beef patty with melted cheese, lettuce, tomato, and pickles.",
                CategoryName = "Burger",
                ImageUrl = "https://i.pinimg.com/736x/8b/36/9f/8b369fefca44952ef36cc09f830c00e7.jpg"
            }, 
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mango Smoothie",
                Price = 4.99,
                Description = "Refreshing mango smoothie made with ripe mangoes and yogurt.",
                CategoryName = "Beverages",
                ImageUrl = "https://i.pinimg.com/736x/d2/2c/49/d22c49a7b5309f97eb57e0dda6304a1b.jpg"
            });
        }
    }
}
