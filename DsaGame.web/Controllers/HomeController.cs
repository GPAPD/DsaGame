using DsaGame.BackendApi.Service.IService;
using DsaGame.Web.Models;
using DsaGame.Web.Models.dtos;
using DsaGame.Web.Service.IService;
using DsaGame.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace DsaGame.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBananaService _bananaService;
		private readonly IScoreData _scoreData;

		public HomeController(ILogger<HomeController> logger, IBananaService bananaService, IScoreData scoreData) 
		{
			_logger = logger;
			_bananaService = bananaService;
			_scoreData = scoreData;
		}

        [Authorize]
        public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[HttpGet]
        public async Task<IActionResult> MathGameUI(int level = 1) 
		{
			MathGameModel model = new();
			// banana API call
            var result = await _bananaService.GetBananaApi();

            if (result != null && result.IsSuccess && result.Result != null) 
			{
				//deserializeing banaAPI result
                model.BananaResponse = JsonConvert.DeserializeObject<BananaResponse>(JsonConvert.SerializeObject(result.Result));

				model.Answer = HashingTheSolution(model.BananaResponse.Solution);
				model.Level = level;
            }

            return PartialView(model);
		}

		[HttpGet]
		public async Task<IActionResult> GameOverScreen(int userLevel) 
		{
            GameOverScreenModel model = new();

			if (userLevel > 1)
			{
				int lastLevel = userLevel - 1;
				int ponts = lastLevel * SD.BasicPoint;

				ScoreModel scoreModel = new ScoreModel
				{
					Email = User.Identity.Name,
					GameLevel = userLevel,
					Points = ponts,
					IsLegit = true,
					RecordDate = DateTime.Now,
				};

				var data = await _scoreData.SetNewScore(scoreModel);

                model.PageTitle = SD.GameOver;
                model.GameOverPageButtonTitle = SD.TryAgain;
            }
			else 
			{
				model.PageTitle = SD.ScoreBoard;
				model.GameOverPageButtonTitle = SD.Home;
            }
			var scoreData = await _scoreData.GetScoreTopScores(10);
			model.ScoreList = JsonConvert.DeserializeObject<List<ScoreModel>>(scoreData.Result.ToString());

            return PartialView(model);
		}

        [HttpGet]
		public bool ChecKUserAnswer(string userAnswer, string hashSolution) 
		{
			if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrEmpty(hashSolution)) 
			{
				bool isAnswer = MatchHashValue(userAnswer, hashSolution);

				return isAnswer;
            }

			return false;
		}

        #region answer hashing and hash answer checking methords

        /*encode decode .net doc
        * https://learn.microsoft.com/en-us/dotnet/standard/security/ensuring-data-integrity-with-hash-codes
        */

		//TODO: Need to add the documentation to this hashing methors 
        private string HashingTheSolution(string solution) 
		{
			if (string.IsNullOrEmpty(solution)) 
			{
				return null;
			}

            byte[] hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(solution));
            string val = Convert.ToHexString(hashValue);

            return val;
		}


		private bool MatchHashValue(string answer, string hashVal) 
		{
			if (answer != null && hashVal != null) 
			{
				byte[] sentHashValue = Convert.FromHexString(hashVal);

				var compareHashValue = SHA256.HashData(Encoding.UTF8.GetBytes(answer));

				bool same = sentHashValue.SequenceEqual(compareHashValue);

				return same;
            }
		
			return false;
		}
        #endregion

    }
}
