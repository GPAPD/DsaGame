using DsaGame.BackendApi.Model.Dto;

namespace DsaGame.BackendApi.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationDto registrationDto);

        Task<LogInResponceDto> Login(LogInRequestDto logInRequestDto);

        Task<bool> AssignRole(string email, string roleName);
    }
}
