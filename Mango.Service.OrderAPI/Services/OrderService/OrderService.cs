using AutoMapper;
using Mango.Service.OrderAPI.Data.Context;
using Mango.Service.OrderAPI.Data.Entities;
using Mango.Service.OrderAPI.Models.DTOs;
using Mango.Service.OrderAPI.Utilities;
using MangoFood.Service.OrderAPI.Models.Common;
using MangoFood.Service.OrderAPI.Services.ProductService;

namespace Mango.Service.OrderAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext context, IProductService productService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateOrder(OrderDto orderDto)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                order.OrderTime = DateTime.Now;
                order.Status = SD.Status_Pending;

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Order Successfully";

                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }
    }
}
