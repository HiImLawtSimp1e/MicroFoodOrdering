using System.ComponentModel.DataAnnotations;

namespace MangoFood.UI.Models.DTOs.AuthDTO
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
