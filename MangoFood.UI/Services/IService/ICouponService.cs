using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.CouponDTO;

namespace MangoFood.UI.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(Guid id);
        Task<ResponseDto?> CreateCouponsAsync(CreateCouponDto couponDto);
        Task<ResponseDto?> UpdateCouponsAsync(Guid id, UpdateCouponDto couponDto);
        Task<ResponseDto?> DeleteCouponsAsync(Guid id);
    }
}
