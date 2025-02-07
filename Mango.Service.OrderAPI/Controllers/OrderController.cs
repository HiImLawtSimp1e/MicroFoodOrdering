using AutoMapper;
using Azure;
using Mango.Service.OrderAPI.Data.Context;
using Mango.Service.OrderAPI.Data.Entities;
using Mango.Service.OrderAPI.Models.DTOs;
using Mango.Service.OrderAPI.Services.OrderService;
using Mango.Service.OrderAPI.Utilities;
using MangoFood.Service.OrderAPI.Models.Common;
using MangoFood.Service.OrderAPI.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Service.OrderAPI.Controllers
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
