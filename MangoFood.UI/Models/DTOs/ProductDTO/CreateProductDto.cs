using System.ComponentModel.DataAnnotations;

namespace MangoFood.UI.Models.DTOs.ProductDTO
{
	public class CreateProductDto
	{
		[Required]
		public string Name { get; set; } = string.Empty;
		[Range(1, 1000)]
		public double Price { get; set; }
		public string Description { get; set; } = string.Empty;
		public string CategoryName { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
	}
}
