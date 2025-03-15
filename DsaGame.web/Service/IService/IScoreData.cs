using DsaGame.Web.Models;
using DsaGame.Web.Models.dtos;

namespace DsaGame.BackendApi.Service.IService
{
    public interface IScoreData
    {
        public Task<ResponesDto> GetScoreBordByDate(DateTime date);
        public Task<ResponesDto> GetScoreTopScores(int count);

        /// <summary>
        /// Set new game score 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public Task<ResponesDto> SetNewScore(ScoreModel score);

        public Task<ResponesDto> GetPersonalScore(string email);

    }
}
