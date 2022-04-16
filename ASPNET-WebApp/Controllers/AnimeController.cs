using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class AnimeController : BaseController
    {
        private readonly IAnimeService animeService;

        public AnimeController(IAnimeService _animeService)
        {
            animeService = _animeService;
        }

        // GET: AnimeController
        public IActionResult Index()
        {
            return View();
        }

        // GET: AnimeController/Details/5
        public async Task<IActionResult> ManageAnimes()
        {
            var animes = await animeService.GetAnimes();

            return View(animes);
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

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    model.Image = dataStream.ToArray();
                }
            }

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
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: AnimeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                //if (Request.Form.Files.Count > 0)
                //{
                //    IFormFile file = Request.Form.Files.FirstOrDefault();
                //    using (var dataStream = new MemoryStream())
                //    {
                //        await file.CopyToAsync(dataStream);
                //        user.ProfilePicture = dataStream.ToArray();
                //    }
                //    await _userManager.UpdateAsync(user);
                //}

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
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
