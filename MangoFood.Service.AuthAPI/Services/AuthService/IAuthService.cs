using MangoFood.Service.AuthAPI.Models.Common;
using MangoFood.Service.AuthAPI.Models.DTOs;

namespace MangoFood.Service.AuthAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterDto registerDto);
        Task<ServiceResponse<string>> Login(LoginDto loginDto);
    }
}
