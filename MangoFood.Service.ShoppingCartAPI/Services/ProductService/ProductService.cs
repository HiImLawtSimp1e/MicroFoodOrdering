using MangoFood.Service.ShoppingCartAPI.Models.Common;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;
using Newtonsoft.Json;

namespace MangoFood.Service.ShoppingCartAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<ProductResponseDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");

            var apiContext = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponseDto>(apiContext);

            if (res.Success)
            {
                return JsonConvert.DeserializeObject<List<ProductResponseDto>>(Convert.ToString(res.Data));
            }
            return new List<ProductResponseDto>();
        }
    }
}
