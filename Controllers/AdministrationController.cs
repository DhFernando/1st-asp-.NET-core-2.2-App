﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles); 
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(String id)
        {
            var role = await roleManager.FindByIdAsync(id);
            
            if(role == null)
            {
                ViewBag.ErrorMessage = "Error";
                return View("NotFound");
            }
            
            var model = new EditRoleViewModel{  Id = role.Id, RoleName = role.Name  };
            
            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user , role.Name))
                {
                    model.Users.Add(user.UserName); 
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = "Error";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole","Administration");
                }
                

                return View(model);
            }
        }

        public async Task<IActionResult> DeleteRole(String id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {

            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                else { }
            }
            return RedirectToAction("ListRole", "Administration");
        }


        [HttpGet]
        public async Task<IActionResult> EditUserInRole( String roleId )
        {
            ViewBag.roleId = roleId;
            
            var role = await roleManager.FindByIdAsync(roleId);
            ViewBag.role = role;
            if(role == null)
            {
                
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model , String roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
            {

            }

            for(var i=0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if(model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!(model[i].IsSelected) && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count) - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
   
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        public async Task<IActionResult> EditRoleInUser(String Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var model = new List<UserRoleViewModel>();

            ViewBag.User = user;

            if (user == null)
            {

            }

            foreach (var role in roleManager.Roles)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = Id,
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditRoleInUser(List<UserRoleViewModel> model, String userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            ViewBag.User = userId;

            if (user == null)
            {

            }

            for (var i = 0; i < model.Count; i++)
            {
                var role = await roleManager.FindByIdAsync(model[i].RoleId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(model[i].IsSelected) && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count) - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditUser", new { Id = userId });
                    }
                }

            }
            return RedirectToAction("EditUser", new { Id = userId });
        }



        public IActionResult ListUsers()
        {
            var model = userManager.Users;
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user =await userManager.FindByIdAsync(id);
            
            if(user == null)
            {

            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.Email,
                City = user.City,
                Email = user.Email,
                Roles = userRoles,
                Claims = userClaims.Select(c=>c.Value).ToList()

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {

            }
            else
            {
                user.UserName = model.Email;
                user.City = model.City;
                user.Email = model.Email;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
            }


            return View(model);
        }

        public async Task<IActionResult> DeleteUser(String id)
        {
            var user = await userManager.FindByIdAsync(id);
            if(user == null)
            {

            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }
                else
                {

                }
            }

            return RedirectToAction("ListUsers", "Administration");
        }

        

    }
}
