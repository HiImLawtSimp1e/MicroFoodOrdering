using AutoMapper;
using MangoFood.Service.ShoppingCartAPI.Data.Context;
using MangoFood.Service.ShoppingCartAPI.Data.Entities;
using MangoFood.Service.ShoppingCartAPI.Models.Common;
using MangoFood.Service.ShoppingCartAPI.Models.DTOs;
using MangoFood.Service.ShoppingCartAPI.Services.CouponService;
using MangoFood.Service.ShoppingCartAPI.Services.ProductService;

namespace MangoFood.Service.ShoppingCartAPI.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public CartService(ApplicationDbContext context, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _context = context;
            _mapper = mapper;
            _productService = productService;
            _couponService = couponService;
        }
        public async Task<ServiceResponse<CartResponseDto>> GetCart(string userId)
        {
            var dbCart = await GetCustomerCart(userId);

            var cart = _mapper.Map<CartResponseDto>(dbCart);

            var products = await _productService.GetProducts();

            foreach (var item in cart.CartItems)
            {
                item.Product = products.FirstOrDefault(u => u.Id == item.ProductId);
                cart.TotalAmount += item.Quantity * item.Product.Price;
            }

            if (!string.IsNullOrEmpty(cart.CouponCode))
            {
                var coupon = await _couponService.GetCoupon(cart.CouponCode);
                if (coupon != null && cart.TotalAmount > coupon.MinAmount)
                {
                    cart.TotalAmount -= coupon.DiscountAmount;
                    cart.Discount = coupon.DiscountAmount;
                }
            }

            return new ServiceResponse<CartResponseDto>
            {
                Data = cart
            };
        }

        public async Task<ServiceResponse<bool>> AddToCart(string userId, CartItemDto cartItem)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var dbCart = await GetCustomerCart(userId);

                //check if cart item has same product
                var cartItemExist = await _context.CartItems
                                              .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId
                                                                   && ci.CartId == dbCart.Id);

                if (cartItemExist == null)
                {
                    //create cart item
                    var newCartItem = _mapper.Map<CartItem>(cartItem);
                    newCartItem.CartId = dbCart.Id;

                    _context.CartItems.Add(newCartItem);
                }
                else
                {
                    //update quantity in cart item
                    cartItemExist.Quantity += cartItem.Quantity;
                }

                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Added item to cart";

                return res;
            }
            catch (Exception ex) 
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
           
        }

        public async Task<ServiceResponse<bool>> RemoveCartItem(Guid cartItemId)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == cartItemId);

                if (cartItem == null)
                {
                    res.Success = false;
                    res.Message = "Not found cart item";

                    return res;
                }

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Removed item from cart successfully!";

                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }

        public async Task<ServiceResponse<bool>> ApplyCoupon(string userId, string couponCode)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var coupon = await _couponService.GetCoupon(couponCode);

                if (coupon == null)
                {
                    res.Success = false;
                    res.Message = "Invalid or expired coupon code";

                    return res;
                }

                var dbCart = await GetCustomerCart(userId);

                dbCart.CouponCode = couponCode;

                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Coupon has been applied";

                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }

        public async Task<ServiceResponse<bool>> RemoveCoupon(string userId)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var dbCart = await GetCustomerCart(userId);

                dbCart.CouponCode = "";

                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Coupon has been removed";

                return res;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return res;
            }
        }

        private async Task<Cart> GetCustomerCart(string userId)
        {
            var dbCart = await _context.Carts
                                    .Include(c => c.CartItems)
                                    .FirstOrDefaultAsync(c => c.UserId == userId);

            if (dbCart == null)
            {
                // If customer cart doesn't exist, create new cart
                dbCart = await CreateCustomerCart(userId);
            }

            return dbCart;
        }

        private async Task<Cart> CreateCustomerCart(string userId)
        {
            var newCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CouponCode = "",
                CartItems = new List<CartItem>()
            };

            _context.Carts.Add(newCart);
            await _context.SaveChangesAsync();

            return newCart;
        }
    }
}
