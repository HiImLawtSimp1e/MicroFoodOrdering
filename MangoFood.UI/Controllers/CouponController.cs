using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.CouponDTO;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MangoFood.UI.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            List<CouponResponseDto>? list = new();

            ResponseDto? response = await _couponService.GetCouponsAsync();

            if (response != null && response.Success)
            {
                list = JsonConvert.DeserializeObject<List<CouponResponseDto>>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

		public async Task<IActionResult> Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateCouponDto item)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponsAsync(item);
                if (response != null && response.Success)
                {
                    TempData["success"] = response?.Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return View(item);
        }

        public async Task<IActionResult> Delete(Guid couponId)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.Success)
            {
                CouponResponseDto? model = JsonConvert.DeserializeObject<CouponResponseDto>(Convert.ToString(response.Data));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CouponResponseDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponsAsync(couponDto.Id);
            if (response != null && response.Success)
            {
                TempData["success"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(couponDto);
        }
    }
}
