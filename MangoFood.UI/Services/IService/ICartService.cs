using MangoFood.UI.Models.DTOs.CartDTO;
using MangoFood.UI.Models.DTOs.Common;

namespace MangoFood.UI.Services.IService
{
    public interface ICartService
    {
        Task<ResponseDto?> GetCartByUserIdAsnyc(string userId);
        Task<ResponseDto?> AddToCartAsync(string userId, CartItemDto cartItem);
        Task<ResponseDto?> RemoveCartItemAsync(Guid cartItemId);
        Task<ResponseDto?> ApplyCouponAsync(string userId, string couponCode);
        Task<ResponseDto?> RemoveCouponAsync(string userId);
    }
}
