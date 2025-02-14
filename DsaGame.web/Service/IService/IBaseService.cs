using DsaGame.Web.Models.dtos;

namespace DsaGame.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponesDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
