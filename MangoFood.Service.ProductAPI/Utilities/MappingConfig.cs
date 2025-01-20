using AutoMapper;
using MangoFood.Service.ProductAPI.Data.Entities;
using MangoFood.Service.ProductAPI.Models.DTOs;

namespace MangoFood.Service.ProductAPI.Utilities
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateProductDto, Product>();
                config.CreateMap<UpdateProductDto, Product>();
                config.CreateMap<Product, ProductResponseDto>();
            });

            return mappingConfig;
        }
    }
}
