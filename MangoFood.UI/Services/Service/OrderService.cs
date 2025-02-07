using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.OrderDTO;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;

namespace MangoFood.UI.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateOrder(OrderDto orderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = orderDto,
                Url = SD.OrderAPIBase + "/Order/CreateOrder"
            });
        }
    }
}
