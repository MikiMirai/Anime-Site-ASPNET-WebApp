using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext dbContext;
        public CommentService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<bool> AddCommentToReview(Comment model)
        {
            var review = await dbContext.Reviews.Where(x => x.Id == model.ReviewId)
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return false;
            }
            try
            {
                Comment comment = new()
                {
                    ReviewId = model.ReviewId,
                    Description = model.Description,
                    Date = DateTime.Now.ToString(),
                    UserId = model.UserId
                };
                review.Comments.Add(comment);

                await dbContext.Comments.AddAsync(comment);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Comment>> GetCommentsByReview(string id)
        {
            var comments = await dbContext.Comments
                .Include(x => x.User)
                .Where(x => x.ReviewId == id)
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comments = await dbContext.Comments
                .Include(x => x.User)
                .Include(x => x.Review)
                .ToListAsync();

            return comments;
        }
    }
}
