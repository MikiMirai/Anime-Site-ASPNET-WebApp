using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewToAnime(Review model);

        Task<IEnumerable<Review>> GetReviews();

        Task<IEnumerable<AnimeReviewsListViewModel>> GetReviewsByAnimeId(string id);
    }
}
