using Azure;
using DsaGame.BackendApi.Model;
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
        [HttpGet("GetScoreBoardByDate")]
        public async Task<IActionResult> GetScoreBoardByDateAsync([FromBody] DateTime dateAndTime)
        {
            if (dateAndTime != null) 
            {
                var temp = DateTime.Now;
                var scoreBoardList = await _scoreData.GetScoreBordByDate(temp);

                if (scoreBoardList.Count > 0 != null)
                {
                    List<ScoreBoardResponseDto> scoreBoardResponse = new List<ScoreBoardResponseDto>();
                    foreach (var score in scoreBoardList) 
                    {
                        ScoreBoardResponseDto scoreBord = new()
                        {
                            User = new()
                            {
                                Id = score.ApplicationUser.Id,
                                Email = score.ApplicationUser.Email,
                                Name = score.ApplicationUser.Name,
                                PhoneNumber = score.ApplicationUser.PhoneNumber
                            },
                            ScoreId = score.ScoreId,
                            GameLevel = score.GameLevel,
                            IsLegit = score.IsLegit,
                            Points = score.Points,
                            RecordDate = score.RecordDate,
                        };
                        scoreBoardResponse.Add(scoreBord);
                    }
                    _response.Result = scoreBoardResponse;
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

        [HttpGet("GetTopScores")]
        public async Task<IActionResult> GetTopScores([FromBody] int count)
        {
            var scoreBoardList = await _scoreData.GetTopScoresAsync(count);

            if (scoreBoardList.Count > 0)
            {
                List<ScoreBoardResponseDto> scoreBoardResponse = new List<ScoreBoardResponseDto>();
                foreach (var score in scoreBoardList)
                {
                    ScoreBoardResponseDto scoreBord = new()
                    {
                        User = new()
                        {
                            Id = score.ApplicationUser.Id,
                            Email = score.ApplicationUser.Email,
                            Name = score.ApplicationUser.Name,
                            PhoneNumber = score.ApplicationUser.PhoneNumber
                        },
                        ScoreId = score.ScoreId,
                        GameLevel = score.GameLevel,
                        IsLegit = score.IsLegit,
                        Points = score.Points,
                        RecordDate = score.RecordDate,
                    };
                    scoreBoardResponse.Add(scoreBord);
                }
                _response.Result = scoreBoardResponse;
                _response.IsSuccess = true;
                _response.Message = "Successfuly recive the scoreBoard";
            }
            else
            {
                _response.Message = "No Record Found";
            }

            return Ok(_response);
        }


        [HttpPost("SetScore")]
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
