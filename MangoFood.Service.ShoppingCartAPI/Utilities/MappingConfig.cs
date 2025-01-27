using AutoMapper;
using MangoFood.Service.ShoppingCartAPI.Data.Entities;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;

namespace MangoFood.Service.ProductAPI.Utilities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Mapping từ Cart sang CartResponseDto
                config.CreateMap<Cart, CartResponseDto>()
                      .ForMember(dest => dest.Discount, opt => opt.Ignore()) // Không có sẵn trong entity
                      .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) // Không có sẵn trong entity
                      .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems ?? new List<CartItem>()));

                // Mapping từ CartItem sang CartItemResponseDto
                config.CreateMap<CartItem, CartItemResponseDto>()
                      .ForMember(dest => dest.Product, opt => opt.Ignore()); // Không có trong entity

                // Mapping từ CartItemDto sang CartItem
                config.CreateMap<CartItemDto, CartItem>()
                      .ForMember(dest => dest.CartId, opt => opt.Ignore()) // Không có trong DTO
                      .ForMember(dest => dest.Id, opt => opt.Ignore()); // Không có trong DTO
            });

            return mappingConfig;
        }
    }
}
