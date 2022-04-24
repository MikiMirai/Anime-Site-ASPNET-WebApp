using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class AnimeController : BaseController
    {
        private readonly IAnimeService animeService;

        private readonly IGenreService genreService;

        private readonly IApplicationDbRepository repo;

        public AnimeController(IAnimeService _animeService, IApplicationDbRepository repo, IGenreService genreService)
        {
            animeService = _animeService;
            this.repo = repo;
            this.genreService = genreService;
        }

        // GET: AnimeController
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AnimeDetails(string id)
        {
            var animes = await animeService.GetAnimeById(id);

            return View(animes);
        }

        // GET
        public async Task<IActionResult> AllAnime()
        {
            var animes = await animeService.GetAnimes();

            return View(animes);
        }

        // GET: AnimeController/Details/5
        public async Task<IActionResult> ManageAnimes()
        {
            var animes = await animeService.GetAnimes();

            return View(animes);
        }

        [HttpGet]
        public async Task<IActionResult> EditGenre(string id)
        {
            ViewBag.animeId = id;
            var anime = await animeService.GetAnimeForEdit(id);

            if (anime == null)
            {
                ViewBag.ErrorMessage = $"Anime with Id = {id} cannot be found";
                return View("NotFound");
            }

            ViewBag.Name = anime.Name;
            var model = new List<ManageAnimeGenreViewModel>();
            List<Genre> genres = genreService.GetGenres().Result.ToList();

            foreach (var genre in genres)
            {
                var manageAnimeGenreViewModel = new ManageAnimeGenreViewModel
                {
                    GenreId = genre.Id,
                    GenreName = genre.Name
                };

                if (await genreService.HasGenre(anime, genre.Name))
                {
                    manageAnimeGenreViewModel.Selected = true;
                }
                else
                {
                    manageAnimeGenreViewModel.Selected = false;
                }

                model.Add(manageAnimeGenreViewModel);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(List<ManageAnimeGenreViewModel> model, string id)
        {
            Anime anime = await animeService.GetAnimeForEdit(id);
            if (anime == null)
            {
                return View();
            }

            List<Genre> genres = genreService.GetGenres().Result.ToList();
            var result = await genreService.RemoveAnimeGenresAsync(anime, genres);
            if (!result)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await genreService.AddAnimeGenresAsync(anime, model);
            if (!result)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction(nameof(ManageAnimes));
        }

        // GET: AnimeController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AnimeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimeCreateViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            if (await animeService.CreateAnime(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageAnimes));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        // GET: AnimeController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var model = await animeService.GetAnimeForEdit(id);

            return View(model);
        }

        // POST: AnimeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimeEditViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            if (await animeService.UpdateAnime(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageAnimes));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        // GET: AnimeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: AnimeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
