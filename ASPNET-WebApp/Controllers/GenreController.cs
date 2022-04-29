using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = RoleConstants.Roles.Admin)]
        public async Task<IActionResult> ManageGenres()
        {
            var genres = await genreService.GetGenres();
            return View(genres);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Roles.Admin)]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await genreService.GetGenreForEdit(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Roles.Admin)]
        public async Task<IActionResult> Edit(GenreEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await genreService.UpdateGenre(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGenres));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Roles.Admin)]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenre(GenreCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await genreService.CreateGenre(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGenres));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }
    }
}
