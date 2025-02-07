using MangoFood.UI.Models.DTOs.CartDTO;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;

namespace MangoFood.UI.Services.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> GetCartByUserIdAsnyc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/Cart/GetCart/" + userId
            });
        }
        public async Task<ResponseDto?> GetOrderByUserIdAsnyc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/Cart/GetOrder/" + userId
            });
        }
        public async Task<ResponseDto?> AddToCartAsync(string userId, CartItemDto cartItem)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartItem,
                Url = SD.ShoppingCartAPIBase + "/Cart/AddToCart/" + userId
            });
        }
        public async Task<ResponseDto?> RemoveCartItemAsync(Guid cartItemId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartItemId,
                Url = SD.ShoppingCartAPIBase + "/Cart/RemoveCartItem/" + cartItemId
            });
        }

        public async Task<ResponseDto?> ApplyCouponAsync(string userId ,string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/Cart/ApplyCoupon/" + userId + "?couponCode=" + couponCode
            });
        }

        public async Task<ResponseDto?> RemoveCouponAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/Cart/RemoveCoupon/" + userId
            });
        }
    }
}
