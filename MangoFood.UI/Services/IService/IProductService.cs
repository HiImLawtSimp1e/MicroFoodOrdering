using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.ProductDTO;

namespace MangoFood.UI.Services.IService
{
	public interface IProductService
	{
		Task<ResponseDto?> GetProductAsync(string productCode);
		Task<ResponseDto?> GetProductsAsync();
		Task<ResponseDto?> GetProductByIdAsync(Guid id);
		Task<ResponseDto?> CreateProductsAsync(CreateProductDto productDto);
		Task<ResponseDto?> UpdateProductsAsync(Guid id, UpdateProductDto productDto);
		Task<ResponseDto?> DeleteProductsAsync(Guid id);
	}
}
