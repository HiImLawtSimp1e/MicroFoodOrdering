using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.ProductDTO;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;

namespace MangoFood.UI.Services.Service
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;

		public ProductService(IBaseService baseService)
        {
			_baseService = baseService;
		}
        public async Task<ResponseDto?> CreateProductsAsync(CreateProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = productDto,
				Url = SD.ProductAPIBase + "/Product"
			});
		}

		public async Task<ResponseDto?> DeleteProductsAsync(Guid id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.ProductAPIBase + "/Product/" + id
			});
		}

		public async Task<ResponseDto?> GetProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/Product"
			});
		}

		public async Task<ResponseDto?> GetProductAsync(string productCode)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/Product/GetByCode/" + productCode
			});
		}

		public async Task<ResponseDto?> GetProductByIdAsync(Guid id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/Product/" + id
			});
		}

		public async Task<ResponseDto?> UpdateProductsAsync(Guid id, UpdateProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.PUT,
				Data = productDto,
				Url = SD.ProductAPIBase + "/Product/" + id
			});
		}
	}
}
