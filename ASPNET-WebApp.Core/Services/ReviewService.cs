using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
	public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddReviewToAnime(Review model)
        {
            var anime = await dbContext.Animes.Where(x => x.Id == model.AnimeId)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync();

            if (anime == null)
            {
                return false;
            }

            try
            {
                Review review = new()
                {
                    AnimeId = anime.Id,
                    Description = model.Description,
                    Date = DateTime.Now.ToString(),
                    Score = model.Score,
                    UserId = model.UserId
                };

                anime.Reviews.Add(review);

                double score = 0;

                foreach (var rating in anime.Reviews)
                {
                    score += rating.Score;
                }

                score /= anime.Reviews.Count;

                anime.Score = score;

                await dbContext.Reviews.AddAsync(review);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            var reviews = await dbContext.Reviews
                .Include(x => x.Comments)
                .Include(x => x.Anime)
                .Include(x => x.User)
                .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetReviewById(string id)
        {
            try
            {
                var review = await dbContext.Reviews
                .Include(x => x.Comments)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                return review;
            }
            catch (Exception)
            {
                throw new ArgumentNullException($"The review with id:{id} was not found.");
            }
        }

        public async Task<IEnumerable<AnimeReviewsListViewModel>> GetReviewsByAnimeId(string id)
		{
			try
			{
                var reviews = await dbContext.Reviews
                .Include(x => x.User)
                .Where(x => x.AnimeId == id)
                .Select(x => new AnimeReviewsListViewModel
                {
                    Id = x.Id,
                    UserName = x.User.UserName,
                    Date = x.Date,
                    Description = x.Description,
                    Score = x.Score,
                    CommentsCount = x.Comments.Count
                }).ToListAsync();

                return reviews;
            }
			catch (Exception)
			{
                throw new ArgumentNullException($"The anime with id:{id} was not found.");
            }
		}
	}
}
