using AutoMapper;
using MangoFood.Service.CouponAPI.Data.Context;
using MangoFood.Service.CouponAPI.Data.Entities;
using MangoFood.Service.CouponAPI.Models.Common;
using MangoFood.Service.CouponAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangoFood.Service.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CouponController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CouponResponseDto>>>> GetCouponsAsync()
        {
            var res = new ServiceResponse<List<CouponResponseDto>>();

            try
            {
                var coupons = await _context.Coupons.ToListAsync();
                var data = _mapper.Map<List<CouponResponseDto>>(coupons);

                res.Data = data;

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
        public async Task<ActionResult<ServiceResponse<CouponResponseDto>>> GetCouponAsync(Guid id)
        {
            var res = new ServiceResponse<CouponResponseDto>();

            try
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);

                if (coupon == null)
                {
                    res.Success = false;
                    res.Message = "Cannot found any coupon";

                    return NotFound(res);
                }

                var data = _mapper.Map<CouponResponseDto>(coupon);
                res.Data = data;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return BadRequest(res);
            }

        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ServiceResponse<CouponResponseDto>>> GetByCode(string code)
        {
            var res = new ServiceResponse<CouponResponseDto>();

            try
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);

                if (coupon == null)
                {
                    res.Success = false;
                    res.Message = "Cannot found any coupon";
                    return NotFound(res);
                }

                var data = _mapper.Map<CouponResponseDto>(coupon);
                res.Data = data;

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
        public async Task<ActionResult<ServiceResponse<bool>>> CreateCoupon(CreateCouponDto item)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var newCoupon = _mapper.Map<Coupon>(item);

                _context.Coupons.Add(newCoupon);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Created coupon successfully";

                return Ok(res);

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message= ex.Message;

                return BadRequest(res);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateCoupon(Guid id, UpdateCouponDto updateCoupon)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var dbCoupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);

                if (dbCoupon == null)
                {
                    res.Success = false;
                    res.Message = "Coupon doesn't exist";
                    return NotFound(res);
                }

                _mapper.Map(updateCoupon, dbCoupon);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Updated coupon successfully";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return BadRequest(res);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCoupon(Guid id)
        {
            var res = new ServiceResponse<bool>();

            try
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);

                if (coupon == null)
                {
                    res.Success = false;
                    res.Message = "Cannot found any coupon";

                    return NotFound(res);
                }

                _context.Remove(coupon);
                await _context.SaveChangesAsync();

                res.Data = true;
                res.Message = "Deleted coupon successfully";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;

                return BadRequest(res);
            }
        }
    }
}
