using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data.Identity;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.UserId);

            if (user != null)
            {
                user.UserName = model.UserName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
