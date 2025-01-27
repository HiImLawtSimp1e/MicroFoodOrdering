using MangoFood.UI.Models.DTOs.ProductDTO;

namespace MangoFood.UI.Models.DTOs.CartDTO
{
    public class CartItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public ProductResponseDto? Product { get; set; }
    }
}
