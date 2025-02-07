using MangoFood.Service.ShoppingCartAPI.Models.Common;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;

namespace MangoFood.Service.ShoppingCartAPI.Services.CartService
{
    public interface ICartService
    {
        public Task<ServiceResponse<CartResponseDto>> GetCart(string userId);
        public Task<ServiceResponse<OrderDto>> GetOrder(string userId);
        public Task<ServiceResponse<bool>> AddToCart(string userId, CartItemDto cartItem);
        public Task<ServiceResponse<bool>> RemoveCartItem(Guid cartItemId);
        public Task<ServiceResponse<bool>> ApplyCoupon(string userId, string couponCode);
        public Task<ServiceResponse<bool>> RemoveCoupon(string userId);
    }
}
