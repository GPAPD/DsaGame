using DsaGame.BackendApi.Data;
using DsaGame.BackendApi.Model;
using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace DsaGame.BackendApi.Service
{
    public class ScoreData : IScoreData
    {
        private readonly AppDbContext _db;
        public ScoreData(AppDbContext db)
        {
            _db = db;
        }
        public async Task <List<ScoreBoard>> GetScoreBordByDate(DateTime date)
        {
            var tomorrow = DateTime.Now.AddDays(1);
            var scoreBoard = await _db.ScoreBoards.Where(d => d.RecordDate >= date && d.RecordDate < tomorrow).ToListAsync();

            return scoreBoard;
        }

        public async Task<bool> SetNewScore(SetScore score)
        {
            try
            {
                if (score != null && score.UserId != null)
                {
                    ScoreBoard scoreBoard = new()
                    {
                        UserId = score.UserId,
                        GameLevel = score.GameLevel,
                        IsLegit = score.IsLegit,
                        Points = score.Points,
                        RecordDate = score.RecordDate,
                    };

                    await _db.ScoreBoards.AddAsync(scoreBoard);
                    _db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
            return false;
        }
    }
}
