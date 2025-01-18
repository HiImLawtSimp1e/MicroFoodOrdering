using System.ComponentModel.DataAnnotations;

namespace MangoFood.Service.AuthAPI.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(\+?\d{1,3})?0?\d{9}$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "Passwords must have at least one non alphanumeric character, at least one digit ('0'-'9') & at least one uppercase ('A'-'Z').")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
