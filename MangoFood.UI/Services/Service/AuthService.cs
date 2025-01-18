using MangoFood.UI.Models.DTOs.AuthDTO;
using MangoFood.UI.Models.DTOs.Common;
using MangoFood.UI.Services.IService;
using MangoFood.UI.Utilities;

namespace MangoFood.UI.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> LoginAsync(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDto,
                Url = SD.AuthAPIBase + "/Auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registerDto,
                Url = SD.AuthAPIBase + "/Auth/register"
            }, withBearer: false);
        }
    }
}
