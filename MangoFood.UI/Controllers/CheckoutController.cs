using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.OrderDTO;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MangoFood.UI.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public CheckoutController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var order = new OrderDto();

            ResponseDto? response = await _cartService.GetOrderByUserIdAsnyc(userId);

            if (response != null && response.Success)
            {
                order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Index(OrderDto orderDto)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var order = new OrderDto();

            ResponseDto? orderResponse = await _cartService.GetOrderByUserIdAsnyc(userId);

            if (orderResponse != null && orderResponse.Success)
            {
                order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(orderResponse.Data));

                orderDto.OrderItems = order.OrderItems;

                var response = await _orderService.CreateOrder(orderDto);

                if (response != null && response.Success)
                {
                    TempData["success"] = response?.Message;

                    return RedirectToAction("Index", "Home");
                }

                TempData["error"] = response?.Message;
                return View(orderDto);
            }
            else
            {
                TempData["error"] = orderResponse?.Message;
                return View(orderDto);
            }
        }
    }
}
