﻿using ASPNET_WebApp.Core.Constants;
using ASPNET_WebApp.Core.Contracts;
using ASPNET_WebApp.Core.Models;
using ASPNET_WebApp.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNET_WebApp.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        public AdminController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await service.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await service.GetUserById(id);
            var model = new RolesManagementViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(RolesManagementViewModel model)
        {
            var user = await service.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.GetUserForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await service.UpdateUser(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRole(string roleName)
        //{
        //    if (roleName != null)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
        //    }

        //    return RedirectToAction(nameof(ManageUsers));
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Editor"
        //    });

        //    return RedirectToAction(nameof(ManageUsers));
        //}
    }
}
