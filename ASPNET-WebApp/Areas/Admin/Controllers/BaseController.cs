using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ASPNET_WebApp.Core.Constants;

namespace ASPNET_WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Roles.Management)]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
