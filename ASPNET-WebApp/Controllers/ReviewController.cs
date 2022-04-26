using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class ReviewController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
