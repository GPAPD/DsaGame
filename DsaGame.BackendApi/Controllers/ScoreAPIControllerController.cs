using Azure;
using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace DsaGame.BackendApi.Controllers
{
    [Route("api/score")]
    [ApiController]
    public class ScoreAPIControllerController : ControllerBase
    {
        private readonly IScoreData _scoreData;
        private readonly ResponseDto _response;

        public ScoreAPIControllerController(IScoreData scoreData)
        {
            _scoreData = scoreData;
            _response = new ResponseDto();
        }

        //TODO: need to add xml summery in Score bord controller
        [HttpGet]
        public async Task<IActionResult> GetScoreBoardByDateAsync(DateTime dateAndTime)
        {

            if (dateAndTime != null) 
            {
                var temp = DateTime.Now;
                var scoreBoard = await _scoreData.GetScoreBordByDate(temp);

                if (scoreBoard.Count > 0 != null)
                {
                    _response.Result = scoreBoard;
                    _response.IsSuccess = true;
                    _response.Message = "Successfuly recive the scoreBoard";
                }
                else 
                {
                    _response.Message = "No Record Found";
                }
            }

            return Ok(_response);
        }


        [HttpPost]
        public async Task<IActionResult> SetScore([FromBody] SetScore score)
        {
  
            if (score != null)
            {
                var scoreBoard = await _scoreData.SetNewScore(score);

                if (scoreBoard)
                {
                    _response.Result = scoreBoard;
                    _response.IsSuccess = true;
                    _response.Message = "score saved successfuly";
                }
                else
                {
                    _response.Message = "Api call faild. An error occurred while seting a new score";
                }
            }

            return Ok(_response);
        }
    }
}
