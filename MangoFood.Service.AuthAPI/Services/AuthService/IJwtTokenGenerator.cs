using MangoFood.Service.AuthAPI.Data.Entities;

namespace MangoFood.Service.AuthAPI.Services.AuthService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, string role);
    }
}
