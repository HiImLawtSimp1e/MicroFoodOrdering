using MangoFood.Service.OrderAPI.Models.DTOs;
using MangoFood.Service.OrderAPI.Services.OrderService;
using MangoFood.Service.OrderAPI.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangoFood.Service.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateOrder(OrderDto orderDto)
        {
            var res = await _orderService.CreateOrder(orderDto);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
