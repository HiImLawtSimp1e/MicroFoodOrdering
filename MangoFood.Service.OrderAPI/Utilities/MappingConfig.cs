using AutoMapper;
using MangoFood.Service.OrderAPI.Data.Entities;
using MangoFood.Service.OrderAPI.Models.DTOs;

namespace MangoFood.Service.OrderAPI.Utilities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderDto, Order>();

                config.CreateMap<OrderItemDto, OrderItem>();

                config.CreateMap<Order, OrderResponseDto>();

                config.CreateMap<OrderItem, OrderItemResponseDto>();
            });

            return mappingConfig;
        }
    }
}
