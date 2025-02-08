using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.OrderDTO;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MangoFood.UI.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetAll(string status)
		{
			var listOrderResponse = new List<OrderResponseDto>();

			ResponseDto response = _orderService.GetAllOrder().GetAwaiter().GetResult();

			if (response != null && response.Success)
			{
				listOrderResponse = JsonConvert.DeserializeObject<List<OrderResponseDto>>(Convert.ToString(response.Data));

				switch (status)
				{
					case "approved":
						listOrderResponse = listOrderResponse.Where(u => u.Status == SD.Status_Approved).ToList();
						break;
					case "readyforpickup":
						listOrderResponse = listOrderResponse.Where(u => u.Status == SD.Status_ReadyForPickup).ToList();
						break;
					case "cancelled":
						listOrderResponse = listOrderResponse.Where(u => u.Status == SD.Status_Cancelled || u.Status == SD.Status_Refunded).ToList();
						break;
					default:
						break;
				}
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return Json(new { data = listOrderResponse });
		}

		[HttpGet]
		public async Task<IActionResult> OrderDetail(Guid orderId)
		{
			var orderResponse = new OrderResponseDto();

			var response = await _orderService.GetOrder(orderId);

			if (response != null && response.Success)
			{
				orderResponse = JsonConvert.DeserializeObject<OrderResponseDto>(Convert.ToString(response.Data));
			}
			else
			{
				TempData["error"] = response?.Message;
			}

			return View(orderResponse);
		}

		[HttpPost("OrderReadyForPickup")]
		public async Task<IActionResult> OrderReadyForPickup(Guid orderId)
		{
            ResponseDto? response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);

			if (response != null && response.Success)
			{
				TempData["success"] = response?.Message;
                return RedirectToAction(nameof(OrderDetail), new { orderId });
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View();
		}

		[HttpPost("CompleteOrder")]
		public async Task<IActionResult> CompleteOrder(Guid orderId)
		{
            ResponseDto? response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);

			if (response != null && response.Success)
			{
				TempData["success"] = response?.Message;
				return RedirectToAction(nameof(OrderDetail), new { orderId });
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View();
		}

		[HttpPost("CancelOrder")]
		public async Task<IActionResult> CancelOrder(Guid orderId)
		{
			ResponseDto? response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Cancelled);

			if (response != null && response.Success)
			{
				TempData["success"] = response?.Message;
				return RedirectToAction(nameof(OrderDetail), new { orderId });
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View();
		}
	}
}
