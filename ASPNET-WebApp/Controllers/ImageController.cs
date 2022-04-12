using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Controllers
{
    public class ImageController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get Image
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }

        //Get a list of Images
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);

            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        //Get Image create page
        public IActionResult Create()
        {
            return View();
        }
    }
}
