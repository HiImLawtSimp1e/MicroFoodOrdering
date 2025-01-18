using MangoFood.UI.Models.DTOs.AuthDTO;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MangoFood.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var loginDto = new LoginDto();
            return View(loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto item)
        {
            ResponseDto result = await _authService.LoginAsync(item);
            if (result != null && result.Success)
            {
                var token = (string)result.Data;

                await SignInUser(token);
                _tokenProvider.SetToken(token);

                TempData["success"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = result.Message;
                return View(item);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto item)
        {
            ResponseDto result = await _authService.RegisterAsync(item);

            if (result != null && result.Success)
            {
                var token = (string)result.Data;

                await SignInUser(token);
                _tokenProvider.SetToken(token);

                TempData["success"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = result.Message;
                var roleList = new List<SelectListItem>()
                {
                    new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                    new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
                };
                ViewBag.RoleList = roleList;

                return View(item);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Invalid JWT token");
            }

            // Giải mã token
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            // Thêm các claims
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            }

            var userName = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(userName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            }

            var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email));
            }

            var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (!string.IsNullOrEmpty(role))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var principal = new ClaimsPrincipal(identity);

            // Lưu thông tin vào cookie để sử dụng sau này
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
