using Azure;
using MangoFood.Service.AuthAPI.Models.Common;
using MangoFood.Service.AuthAPI.Models.DTOs;
using MangoFood.Service.AuthAPI.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace MangoFood.Service.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(RegisterDto req)
        {
            var res = await _authService.Register(req);

            if (res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto req)
        {
            var res = await _authService.Login(req);

            if (res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }
    }
}
