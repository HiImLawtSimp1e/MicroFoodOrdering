using MangoFood.Service.AuthAPI.Data.Context;
using MangoFood.Service.AuthAPI.Data.Entities;
using MangoFood.Service.AuthAPI.Models.Common;
using MangoFood.Service.AuthAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace MangoFood.Service.AuthAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ServiceResponse<string>> Login(LoginDto loginDto)
        {
            var result = new ServiceResponse<string>();

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDto.UserName.ToLower());

            if (user == null) 
            {
                result.Success = false;
                result.Message = "Cannot find username";
                
                return result;
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValid) 
            {
                result.Success = false;
                result.Message = "Wrong password!";

                return result;
            }

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var token = _jwtTokenGenerator.GenerateToken(user, role);

            result.Data = token;
            result.Message = "Login successfully!!";

            return result;
        }

        public async Task<ServiceResponse<string>> Register(RegisterDto registerDto)
        {
            var result = new ServiceResponse<string>();

            var emailExist = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == registerDto.Email.ToLower()
                                                                                 || u.UserName.ToLower() == registerDto.Email.ToLower());

            if (emailExist != null) 
            {
                result.Success = false;

                result.Message = "Username has already exists!";
                return result;
            }

            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                    PhoneNumber = registerDto.PhoneNumber,
                };

                var createUser = await _userManager.CreateAsync(user, registerDto.Password);

                if (createUser.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(registerDto.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerDto.Role));
                    }
                    await _userManager.AddToRoleAsync(user, registerDto.Role);

                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    var token = _jwtTokenGenerator.GenerateToken(user, role);

                    result.Data = token;
                    result.Message = "Registration successfully!!";
                }
                else
                {
                    var errors = string.Join(", ", createUser.Errors.Select(e => e.Description));

                    result.Success = false;
                    result.Message = $"Registration failed: {errors}";
                }
                return result;
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = ex.Message;

                return result;
            }
        }
    }
}
