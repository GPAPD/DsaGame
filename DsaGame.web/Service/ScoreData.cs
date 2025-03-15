using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using DsaGame.Web.Models;
using DsaGame.Web.Models.dtos;
using DsaGame.Web.Service.IService;
using DsaGame.Web.Utility;
using static System.Formats.Asn1.AsnWriter;

namespace DsaGame.Web.Service
{
    public class ScoreData : IScoreData
    {
        private readonly IBaseService _baseService;
        public ScoreData(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponesDto> GetScoreBordByDate(DateTime date)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = date,
                Url = SD.BackEndAPI + "/api/score/GetScoreBoardByDate"
            }, false);
        }

        public async Task<ResponesDto> GetScoreTopScores(int count)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = count,
                Url = SD.BackEndAPI + "/api/score/GetTopScores"
            });
        }

        public async Task<ResponesDto> SetNewScore(ScoreModel score)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = score,
                Url = SD.BackEndAPI + "/api/score/SetScore"
            });
        }

        public async Task<ResponesDto> GetPersonalScore(string email)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = email,
                Url = SD.BackEndAPI + "/api/score/GetPersonalScore"
            });
        }

    }
}
