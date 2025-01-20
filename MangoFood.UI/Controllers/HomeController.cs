using MangoFood.UI.Models;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.ProductDTO;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MangoFood.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
