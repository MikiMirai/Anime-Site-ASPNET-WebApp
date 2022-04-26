using ASPNET_WebApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_WebApp.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> ManageReviews()
        {
            var reviews = await reviewService.GetReviews();

            return View(reviews);
        }
    }
}
