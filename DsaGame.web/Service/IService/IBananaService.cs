using DsaGame.Web.Models.dtos;
using System.Security.Cryptography.X509Certificates;

namespace DsaGame.Web.Service.IService
{
    public interface IBananaService
    {
        public Task<ResponesDto> GetBananaApi();

    }
}
