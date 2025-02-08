using AutoMapper;
using MangoFood.Service.OrderAPI.Data.Context;
using MangoFood.Service.OrderAPI.Data.Entities;
using MangoFood.Service.OrderAPI.Models.DTOs;
using MangoFood.Service.OrderAPI.Utilities;
using MangoFood.Service.OrderAPI.Models.Common;
using MangoFood.Service.OrderAPI.Services.ProductService;
using System.Security.Claims;

namespace MangoFood.Service.OrderAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(ApplicationDbContext context, IProductService productService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<bool>> CreateOrder(OrderDto orderDto)
        {
            var res = new ServiceResponse<bool>();
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                order.OrderTime = DateTime.Now;
                order.Status = SD.Status_Approved;

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

        public async Task<ServiceResponse<List<OrderResponseDto>>> GetOrders()
        {
            var res = new ServiceResponse<List<OrderResponseDto>>();

            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var roleName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                var orders = new List<Order>();

                if(roleName == SD.RoleAdmin)
                {
                    orders = await _context.Orders
                                       .OrderByDescending(o => o.OrderTime)
                                       .ToListAsync();
                }
                else
                {
                    orders = await _context.Orders
                                       .Where(o => o.UserId == userId)
                                       .OrderByDescending(o => o.OrderTime)
                                       .ToListAsync();
                }

                var listOrderResponse = _mapper.Map<List<OrderResponseDto>>(orders);

                res.Data = listOrderResponse;

                return res;
            }
            catch(Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }

        public async Task<ServiceResponse<OrderResponseDto>> GetOrder(Guid orderId)
        {
            var res = new ServiceResponse<OrderResponseDto>();

            try
            {
                var order = new Order();

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var roleName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                if(roleName == SD.RoleAdmin)
                {
                    order = await _context.Orders
                                  .Include(o => o.OrderItems)
                                  .FirstOrDefaultAsync(o => o.Id == orderId);
                }
                else
                {
                    order = await _context.Orders
                                  .Include(o => o.OrderItems)
                                  .Where(o => o.UserId == userId)
                                  .FirstOrDefaultAsync(o => o.Id == orderId);
                }

                if(order == null)
                {
                    res.Success = false;
                    res.Message = "Cannot found your order";

                    return res;
                }

                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                res.Data = orderResponse;

                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }

        public async Task<ServiceResponse<bool>> UpdateOrderStatus(Guid orderId, string newStatus)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var order = await _context.Orders
                                 .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null) 
                {
                    res.Success = false;
                    res.Message = "Cannot found your order";

                    return res;
                }

                order.Status = newStatus;

                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Status updated successfully!!";

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
