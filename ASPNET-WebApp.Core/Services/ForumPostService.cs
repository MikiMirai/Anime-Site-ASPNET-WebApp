using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
    public class ForumPostService : IForumPostService
    {
        private readonly ApplicationDbContext dbContext;
        public ForumPostService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<bool> AddForumPost(ForumPostCreateViewModel model, string userId)
        {
            try
            {
                ForumPost post = new ForumPost()
                {
                    Id = model.Id,
                    UserId = userId,
                    Date = DateTime.Now.ToString(),
                    Description = model.Description
                };

                await dbContext.ForumPost.AddAsync(post);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ForumPostListViewModel>> GetPosts()
        {
            var reviews = await dbContext.ForumPost
                .Include(x => x.User)
                .Select(x => new ForumPostListViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    UserName = x.User.UserName,
                    Description = x.Description
                })
                .ToListAsync();

            return reviews;
        }
    }
}
