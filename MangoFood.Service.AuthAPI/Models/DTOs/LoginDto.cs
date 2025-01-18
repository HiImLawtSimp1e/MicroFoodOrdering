using System.ComponentModel.DataAnnotations;

namespace MangoFood.Service.AuthAPI.Models.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
