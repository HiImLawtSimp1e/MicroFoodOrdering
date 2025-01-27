using MangoFood.Service.ShoppingCartAPI.Models.DTOs;

namespace MangoFood.Service.ShoppingCartAPI.Services.CouponService
{
    public interface ICouponService
    {
        Task<CouponResponseDto> GetCoupon(string couponCode);
    }
}
