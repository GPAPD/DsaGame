using DsaGame.BackendApi.Model;

namespace DsaGame.BackendApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string>roles);
    }
}
