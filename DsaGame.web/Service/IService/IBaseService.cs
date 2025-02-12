using DsaGame.web.Models;

namespace DsaGame.web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponesDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
