using MangoFood.Service.OrderAPI.Models.DTOs;
using MangoFood.Service.OrderAPI.Models.Common;

namespace MangoFood.Service.OrderAPI.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> CreateOrder(OrderDto orderDto);
    }
}
