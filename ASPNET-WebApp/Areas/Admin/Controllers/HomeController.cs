using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
