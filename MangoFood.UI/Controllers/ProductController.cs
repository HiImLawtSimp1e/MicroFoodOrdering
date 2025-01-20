using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Models.DTOs.ProductDTO;
using MangoFood.UI.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MangoFood.UI.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
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

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateProductDto item)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _productService.CreateProductsAsync(item);
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

		public async Task<IActionResult> Update(Guid productId)
		{
			ResponseDto? response = await _productService.GetProductByIdAsync(productId);
			if (response != null && response.Success)
			{
				var dbProduct = JsonConvert.DeserializeObject<UpdateProductDto>(Convert.ToString(response.Data));
				return View(dbProduct);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Update(UpdateProductDto updateProduct)
		{
			ResponseDto? response = await _productService.UpdateProductsAsync(updateProduct.Id, updateProduct);
			if (response != null && response.Success)
			{
				TempData["success"] = response?.Message;
				return RedirectToAction(nameof(Index));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View(updateProduct);
		}

		public async Task<IActionResult> Delete(Guid productId)
		{
			ResponseDto? response = await _productService.GetProductByIdAsync(productId);
			if (response != null && response.Success)
			{
				var product = JsonConvert.DeserializeObject<ProductResponseDto>(Convert.ToString(response.Data));
				return View(product);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> Delete(ProductResponseDto productDto)
		{
			ResponseDto? response = await _productService.DeleteProductsAsync(productDto.Id);
			if (response != null && response.Success)
			{
				TempData["success"] = response?.Message;
				return RedirectToAction(nameof(Index));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View(productDto);
		}

	}
}
