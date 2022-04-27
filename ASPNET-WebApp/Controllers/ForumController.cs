using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using ASPNET_WebApp.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPNET_WebApp.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManagerService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IForumPostService forumPostService;

        public ForumController(IUserService userService, UserManager<ApplicationUser> userManagerService, IHttpContextAccessor httpContextAccessor, IForumPostService forumPostService)
        {
            this.userService = userService;
            this.userManagerService = userManagerService;
            this.httpContextAccessor = httpContextAccessor;
            this.forumPostService = forumPostService;
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(ForumPostCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ClaimsPrincipal? userContext = httpContextAccessor.HttpContext?.User;
            ApplicationUser? user = await userManagerService.GetUserAsync(userContext);


            if (await forumPostService.AddForumPost(model, user.Id))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ForumPosts));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ForumPosts()
        {
            var messages = await forumPostService.GetPosts();

            return View(messages);
        }
    }
}
