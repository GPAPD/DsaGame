using DsaGame.BackendApi.Data;
using DsaGame.BackendApi.Model;
using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var scoreBoard = await _db.ScoreBoards
                .Where(d => d.RecordDate >= date && d.RecordDate < tomorrow)
                .Include(a => a.ApplicationUser)
                .Take(10)
                .OrderByDescending(d => d.Points)
                .ToListAsync();

            return scoreBoard;
        }

        public async Task<List<ScoreBoard>> GetTopScoresAsync(int count)
        {
            var scoreBoard = await _db.ScoreBoards
                .Include(a => a.ApplicationUser)
                .OrderByDescending(a => a.Points)
                .Take(count)
                .ToListAsync();

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
