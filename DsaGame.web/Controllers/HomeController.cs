using DsaGame.Web.Models;
using DsaGame.Web.Models.dtos;
using DsaGame.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace DsaGame.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IBananaService _bananaService;

		public HomeController(ILogger<HomeController> logger, IBananaService bananaService) 
		{
			_logger = logger;
			_bananaService = bananaService;
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
        public async Task<IActionResult> MathGameUI(int level = 0) 
		{
			MathGameModel model = new();
            var result = await _bananaService.GetBananaApi();

            if (result != null && result.IsSuccess) 
			{

                var obj = JsonConvert.DeserializeObject<BananaResponse>(JsonConvert.SerializeObject(result.Result));
				model.BananaResponse = obj;

            }

            return PartialView(model);
		}

		[HttpGet]
		public async Task<IActionResult> GameOverScreen() 
		{
			//var model = 

			return PartialView();
		}


    }
}
