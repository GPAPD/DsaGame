using DsaGame.web.Models;

namespace DsaGame.web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponesDto> LoginAsync(LogInRequestDto logInRequestDto);
        Task<ResponesDto> RegisterAsync(RegistrationDto registrationDto);
        Task<ResponesDto> AssignRoleAsync(RegistrationDto registrationDto);
    }
}
