using MangoFood.Service.ShoppingCartAPI.Models.Common;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;
using MangoFood.Service.ShoppingCartAPI.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangoFood.Service.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<ActionResult<ServiceResponse<CartResponseDto>>> GetCart(string userId)
        {
            var res = await _cartService.GetCart(userId);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("AddToCart/{userId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(string userId, CartItemDto cartItem)
        {
            var res = await _cartService.AddToCart(userId, cartItem);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("RemoveCartItem/{cartItemId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCartItem(Guid cartItemId)
        {
            var res = await _cartService.RemoveCartItem(cartItemId);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPost("ApplyCoupon/{userId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> ApplyCoupon(string userId, string couponCode)
        {
            var res = await _cartService.ApplyCoupon(userId, couponCode);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
        [HttpPost("RemoveCoupon/{userId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCoupon(string userId)
        {
            var res = await _cartService.RemoveCoupon(userId);

            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
