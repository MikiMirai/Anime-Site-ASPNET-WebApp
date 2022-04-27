using ASPNET_WebApp.Infrastructure.Data;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface ICommentService
    {
        Task<bool> AddCommentToReview(Comment model);

        Task<IEnumerable<Comment>> GetComments();

        Task<List<Comment>> GetCommentsByReview(string id);
    }
}
