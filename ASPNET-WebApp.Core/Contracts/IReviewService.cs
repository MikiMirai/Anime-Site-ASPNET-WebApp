using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewToAnime(Review model);

        Task<IEnumerable<Review>> GetReviews();

        Task<Review> GetReviewById(string id);

        Task<List<AnimeReviewsListViewModel>> GetReviewsByAnimeId(string id);
    }
}
