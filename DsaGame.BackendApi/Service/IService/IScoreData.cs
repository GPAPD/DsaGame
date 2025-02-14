using DsaGame.BackendApi.Model;
using DsaGame.BackendApi.Model.Dto;

namespace DsaGame.BackendApi.Service.IService
{
    public interface IScoreData
    {
        public Task<List<ScoreBoard>> GetScoreBordByDate(DateTime date);

        public Task<List<ScoreBoard>> GetTopScoresAsync(int count);

        public Task<bool> SetNewScore(SetScore score);

    }
}
