using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data.Identity;

namespace ASPNET_WebApp.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);
    }
}
