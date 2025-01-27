using MangoFood.UI.Models;
using MangoFood.UI.Models.DTOs.CartDTO;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.ProductDTO;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace MangoFood.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = new List<ProductResponseDto>();

            ResponseDto? response = await _productService.GetProductsAsync();

            if (response != null && response.Success)
            {
                products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(JsonConvert.SerializeObject(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(Guid productId)
        {
            var product = new ProductResponseDto();

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.Success)
            {
                product = JsonConvert.DeserializeObject<ProductResponseDto>(Convert.ToString(response.Data));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductDetails(ProductResponseDto productDto)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                TempData["error"] = "userId null";
            }

            var cartItem = new CartItemDto
            {
                ProductId = productDto.Id,
                Quantity = productDto.Count
            };

            ResponseDto? response = await _cartService.AddToCartAsync(userId, cartItem);


            if (response != null && response.Success)
            {
                TempData["success"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response?.Message;
            return View(productDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
