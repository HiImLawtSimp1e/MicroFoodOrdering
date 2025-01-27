using MangoFood.UI.Models.DTOs.CartDTO;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MangoFood.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            var cart = new CartResponseDto();
            
            ResponseDto? response = await _cartService.GetCartByUserIdAsnyc(userId);

            if (response != null && response.Success)
            {
                cart = JsonConvert.DeserializeObject<CartResponseDto>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(cart);
        }
        public async Task<IActionResult> Remove(Guid cartItemId)
        {
            ResponseDto? response = await _cartService.RemoveCartItemAsync(cartItemId);

            if (response != null & response.Success)
            {
                TempData["success"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response?.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartResponseDto item)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            ResponseDto? response = await _cartService.ApplyCouponAsync(userId, item.CouponCode);

            if (response != null & response.Success)
            {
                TempData["success"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = response?.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartResponseDto item)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            ResponseDto? response = await _cartService.RemoveCouponAsync(userId);

            if (response != null & response.Success)
            {
                TempData["success"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = response?.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
