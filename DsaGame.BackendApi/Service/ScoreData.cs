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
        //Get Score bord By Date
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

        //Get Top 10 Scores
        public async Task<List<ScoreBoard>> GetTopScoresAsync(int count)
        {
            try
            {
                var scoreBoard = await _db.ScoreBoards
                    .Include(a => a.ApplicationUser)
                    .OrderByDescending(a => a.Points)
                    .Take(count)
                    .ToListAsync();

                return scoreBoard;

            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        //Set New Score
        public async Task<bool> SetNewScore(SetScore score)
        {
            try
            {
                var user = _db.ApplicationUsers.FirstOrDefault(a => a.Email.ToLower() == score.ApplicationUser.Email.ToLower());
                if (score != null && score.ApplicationUser != null)
                {
                    ScoreBoard scoreBoard = new()
                    {
                        UserId = user.Id,
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



        //Personal Score Data
        public async Task<List<ScoreBoard>> GetPersonalScore(string Email)
        {
            try
            {
                var scoreBord = await _db.ScoreBoards.Include(u => u.ApplicationUser)
                    .Where(a => a.ApplicationUser.Email == Email)
                    .OrderByDescending(a => a.Points)
                    .Take(10)
                    .ToListAsync();

                return scoreBord;
            }
            catch (Exception e) 
            {
                return null;
            }
        }

    }
}
