using Microsoft.AspNetCore.Identity;

namespace MangoFood.Service.AuthAPI.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
