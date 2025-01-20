using System.ComponentModel.DataAnnotations;

namespace MangoFood.Service.ProductAPI.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
