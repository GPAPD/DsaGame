using DsaGame.BackendApi.Data;
using DsaGame.BackendApi.Model;
using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace DsaGame.BackendApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator; 
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
            if (user != null) 
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult()) 
                {
                    //Create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;
            }
            return false;
        }

        public async Task<LogInResponceDto> Login(LogInRequestDto logInRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(a => a.Email.ToLower() == logInRequestDto.Email.ToLower());
            var isValid = await _userManager.CheckPasswordAsync(user, logInRequestDto.Password);

            if (user == null || isValid == false) 
            {
                return new LogInResponceDto() {User = null, Token="" };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var Token = _jwtTokenGenerator.GenerateToken(user,roles);
            UserDto userDto = new() 
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
            };
            return new LogInResponceDto() { User = userDto, Token = Token };

        }

        public async Task<string> Register(RegistrationDto registrationDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationDto.Name,
                Email = registrationDto.Email,
                NormalizedEmail = registrationDto.Email.ToUpper(),
                Name = registrationDto.Name,
                PhoneNumber = registrationDto.PhoneNumber,
            };

            try 
            {
                var result = await _userManager.CreateAsync(user,registrationDto.Password);
                if (result.Succeeded)
                {
                    var newUser = _db.ApplicationUsers.FirstOrDefault(u => u.Email == registrationDto.Email);

                    UserDto userDto = new()
                    {
                        Email = newUser.Email,
                        Name = newUser.Name,
                        Id = newUser.Id,
                        PhoneNumber = newUser.PhoneNumber

                    };

                    return "";
                }
                else 
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex) 
            {
            
            }

            return "Error Occurred";
        }
    }
}
