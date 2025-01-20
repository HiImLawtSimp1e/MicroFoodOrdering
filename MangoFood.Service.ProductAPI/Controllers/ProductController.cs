using AutoMapper;
using MangoFood.Service.ProductAPI.Data.Context;
using MangoFood.Service.ProductAPI.Data.Entities;
using MangoFood.Service.ProductAPI.Models.Common;
using MangoFood.Service.ProductAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangoFood.Service.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ProductResponseDto>>>> GetProductsAsync()
        {
            var res = new ServiceResponse<List<ProductResponseDto>>();

            try
            {
                var products = await _context.Products.ToListAsync();
                var productsResponse = _mapper.Map<List<ProductResponseDto>>(products);

                res.Data = productsResponse;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return BadRequest(res);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductResponseDto>>> GetProductAsync(Guid id)
        {
            var res = new ServiceResponse<ProductResponseDto>();

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if(product == null)
                {
                    res.Success = false;
                    res.Message = "Can not found any product!!";

                    return NotFound(res);
                }

                var productResponse = _mapper.Map<ProductResponseDto>(product);

                res.Data = productResponse;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<bool>> CreateProduct(CreateProductDto item)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var newProduct = _mapper.Map<Product>(item);
                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Created product successfully!";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return BadRequest(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<bool>> UpdateProduct(Guid id, UpdateProductDto updateProduct)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (dbProduct == null)
                {
                    res.Success = false;
                    res.Message = "Can not found any product!!";

                    return NotFound(res);
                }

                _mapper.Map(updateProduct, dbProduct);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Updated product successfully!";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return BadRequest(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<bool>> DeleteProduct(Guid id)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    res.Success = false;
                    res.Message = "Can not found any product!!";

                    return NotFound(res);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Product has been deleted";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
            }
            return BadRequest(res);
        }
    }
}
