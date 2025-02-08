using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.OrderDTO;

namespace MangoFood.UI.Services.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrder(OrderDto orderDto);
        Task<ResponseDto?> GetAllOrder();
        Task<ResponseDto?> GetOrder(Guid orderId);
        Task<ResponseDto?> UpdateOrderStatus(Guid orderId, string newStatus);
    }
}
