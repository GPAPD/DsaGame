using DsaGame.Web.Models.dtos;
using DsaGame.Web.Service.IService;
using DsaGame.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DsaGame.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public ActionResult Login()
        {
            LogInRequestDto logInRequestDto = new LogInRequestDto();

            return View(logInRequestDto);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LogInRequestDto obj)
        {
            ResponesDto respose = await _authService.LoginAsync(obj);

            if (respose != null && respose.IsSuccess)
            {
                LogInResponceDto logInResponceDto = 
                    JsonConvert.DeserializeObject<LogInResponceDto>(Convert.ToString(respose.Result));

                if (logInResponceDto != null && logInResponceDto.Token != null) 
                {
                    await SignInUser(logInResponceDto);
                    _tokenProvider.SetToken(logInResponceDto.Token);
                }
                return RedirectToAction("Index", "Home");
            }
            else if (respose != null && !respose.IsSuccess)
            {
                ModelState.AddModelError("CustomError", respose.Message);

            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var rolelist = new List<SelectListItem>
            {
                new SelectListItem{Text=SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value = SD.RoleCustomer}
            };
            ViewBag.rolelist = rolelist;

            return View();
        }
        [HttpPost]
        public async  Task<ActionResult> Register(RegistrationDto obj)
        {
            ResponesDto respose = await _authService.RegisterAsync(obj);
            ResponesDto assignRole;

            if (respose != null && respose.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;

                }

                assignRole = await _authService.AssignRoleAsync(obj);
                if (assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else if (respose != null && !respose.IsSuccess)
            { 
                TempData["error"] = $"Registration Fail {respose.Message}";
            
            }
            var rolelist = new List<SelectListItem>
            {
                new SelectListItem{Text=SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value = SD.RoleCustomer}
            };
            ViewBag.rolelist = rolelist;

            return View(obj);
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            return PartialView();
        }


        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.DeleteToken();

            return RedirectToAction("Index","Home");
        }

        private async Task SignInUser(LogInResponceDto model) 
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
