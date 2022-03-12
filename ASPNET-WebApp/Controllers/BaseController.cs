using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
