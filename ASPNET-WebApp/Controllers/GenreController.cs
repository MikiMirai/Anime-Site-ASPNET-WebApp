using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IAnimeService animeService;
        private readonly IGenreService genreService;
        public GenreController(IAnimeService _animeService, IGenreService _genreService)
        {
            animeService = _animeService;
            genreService = _genreService;
        }

        public async Task<IActionResult> ManageGenres()
        {
            var genres = await genreService.GetGenres();
            return View(genres);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await genreService.GetGenreForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await genreService.UpdateGenre(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddGenre()
        {
            return View();
        }

        // POST: GameController/Create
        [HttpPost]
        //[Authorize(Roles = RoleConstants.Roles.Both)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenre(string name)
        {

            if (await genreService.CreateGenre(name))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGenres));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View();
        }
    }
}
