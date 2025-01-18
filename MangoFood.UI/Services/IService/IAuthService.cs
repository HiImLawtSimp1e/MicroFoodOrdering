using MangoFood.UI.Models.DTOs.AuthDTO;
using MangoFood.UI.Models.DTOs.Common;

namespace MangoFood.UI.Services.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginDto loginDto);
        Task<ResponseDto?> RegisterAsync(RegisterDto registerDto);
    }
}
