using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data.Identity;
using ASPNET_WebApp.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;
        private readonly UserManager<ApplicationUser> userManager;

		public UserService(IApplicationDbRepository _repo, UserManager<ApplicationUser> userManager)
		{
			repo = _repo;
			this.userManager = userManager;
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
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                if (user == null)
                {
                    throw new ArgumentNullException($"Unable to load user with id:{model.Id}.");
                }

                if (model.UserName != user.UserName)
                {
                    user.UserName = model.UserName;
                    await userManager.UpdateAsync(user);

                    result = true;
                }
            }
			else
			{
                throw new ArgumentNullException("The user edit model was null.");
			}

            return result;
        }
    }
}
