using MangoFood.Service.ShoppingCartAPI.Models.DTOs;

namespace MangoFood.Service.ShoppingCartAPI.Services.ProductService
{
    public interface IProductService
    {
        public Task<List<ProductResponseDto>> GetProducts();
    }
}
