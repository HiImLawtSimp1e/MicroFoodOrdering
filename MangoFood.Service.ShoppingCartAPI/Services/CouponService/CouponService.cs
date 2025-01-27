using MangoFood.Service.ShoppingCartAPI.Models.Common;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;
using Newtonsoft.Json;

namespace MangoFood.Service.ShoppingCartAPI.Services.CouponService
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponResponseDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");

            var apiContext = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponseDto>(apiContext);

            return JsonConvert.DeserializeObject<CouponResponseDto>(Convert.ToString(res.Data));
        }
    }
}
