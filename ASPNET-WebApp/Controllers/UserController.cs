using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
