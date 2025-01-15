using AutoMapper;
using MangoFood.Service.CouponAPI.Data.Entities;
using MangoFood.Service.CouponAPI.Models.DTOs;

namespace MangoFood.Service.CouponAPI.Utilities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateCouponDto, Coupon>();
                config.CreateMap<UpdateCouponDto, Coupon>();
                config.CreateMap<Coupon, CouponResponseDto>();
            });

            return mappingConfig;
        }
    }
}
