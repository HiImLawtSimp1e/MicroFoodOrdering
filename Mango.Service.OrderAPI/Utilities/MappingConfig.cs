using AutoMapper;
using Mango.Service.OrderAPI.Data.Entities;
using Mango.Service.OrderAPI.Models.DTOs;

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
            });

            return mappingConfig;
        }
    }
}
