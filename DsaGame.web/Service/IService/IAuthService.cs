using DsaGame.Web.Models.dtos;

namespace DsaGame.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponesDto> LoginAsync(LogInRequestDto logInRequestDto);
        Task<ResponesDto> RegisterAsync(RegistrationDto registrationDto);
        Task<ResponesDto> AssignRoleAsync(RegistrationDto registrationDto);
    }
}
