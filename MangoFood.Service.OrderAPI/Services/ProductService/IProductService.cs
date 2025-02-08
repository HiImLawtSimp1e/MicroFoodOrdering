using MangoFood.Service.OrderAPI.Models.DTOs;

namespace MangoFood.Service.OrderAPI.Services.ProductService
{
    public interface IProductService
    {
        public Task<List<ProductResponseDto>> GetProducts();
    }
}
