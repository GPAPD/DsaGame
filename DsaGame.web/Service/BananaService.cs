using DsaGame.Web.Models;
using DsaGame.Web.Service.IService;
using DsaGame.Web.Utility;

namespace DsaGame.Web.Service
{
    public class BananaService : IBananaService
    {
        private readonly IBaseService _baseService;
        public BananaService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponesDto> GetBananaApi()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BananaAPIBase + "/uob/banana/api.php"
            },false);
        }
    }
}
