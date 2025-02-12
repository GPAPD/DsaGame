using DsaGame.BackendApi.Model.Dto;
using DsaGame.BackendApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace DsaGame.BackendApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIControllerController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIControllerController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            var returnErrors = await _authService.Register(model);

            if (!string.IsNullOrEmpty(returnErrors))
            {
                _response.Message = returnErrors.ToString();
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInRequestDto logInRequestDto)
        {

            var logInRes = await _authService.Login(logInRequestDto);
            if (logInRes.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Incorecct username or password";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Result = logInRes;

            return Ok(_response);
        }


        [HttpPost("assign_role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationDto model)
        {

            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());

            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Occurred";
                return BadRequest(_response);
            }
            _response.IsSuccess = true;
            _response.Result = assignRoleSuccessful;

            return Ok(_response);
        }
    }
}
