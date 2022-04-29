using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNET_WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimeService animeService;

        public HomeController(ILogger<HomeController> logger, IAnimeService animeService)
        {
            _logger = logger;
            this.animeService = animeService;
        }

        public async Task<IActionResult> Index()
        {
            var animes = await animeService.GetAnimesTrending();

            return View(animes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}