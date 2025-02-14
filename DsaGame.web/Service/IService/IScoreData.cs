
using DsaGame.Web.Models.dtos;

namespace DsaGame.BackendApi.Service.IService
{
    public interface IScoreData
    {
        public Task<ResponesDto> GetScoreBordByDate(DateTime date);
        public Task<ResponesDto> GetScoreTopScores(int count);

        ///public Task<ResponesDto> SetNewScore(Score score);

    }
}
