using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IForumPostService
    {
        Task<bool> CreateForumPost(ForumPostCreateViewModel model, string userId);

        Task<IEnumerable<ForumPostListViewModel>> GetPosts();
    }
}
