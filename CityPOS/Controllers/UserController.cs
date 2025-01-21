using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace CityPOS.Controllers
{
   
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController
           (SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

       

        public IActionResult Create()
        {
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AspNetUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
               

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    }
                  

                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction(nameof(Create)); 
                }

                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                TempData["ErrorMessage"] = "User creation failed. Please check the errors below.";
            }

            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles;

            return View(model);
        }
        public IActionResult List()
        {
            var users = _userManager.Users.Select(user => new
            {
                User = user,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToList();

            
            var model = users.Select(u => new UserListViewModel
            {
                UserId = u.User.Id,
                UserName = u.User.UserName,
                Email = u.User.Email,
                Roles = string.Join(", ", u.Roles) 
            }).ToList();

            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Invalid user ID.";
                return RedirectToAction(nameof(List));
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(List));
            }

           
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting user.";
            }

            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> EditUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Invalid user ID.";
                return RedirectToAction("UserList");
            }

            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("UserList");
            }

           
            var userRoles = await _userManager.GetRolesAsync(user);
            var currentRole = userRoles.FirstOrDefault();
           
            var model = new UserListViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = currentRole 
            };

            ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserListViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return RedirectToAction("List");
                }
                user.UserName = model.UserName;
                user.Email = model.Email;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToList();

                    return View("EditUser", model);
                }

                
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (!string.IsNullOrEmpty(model.Roles) && !currentRoles.Contains(model.Roles))
                {
                    if (currentRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }
                    await _userManager.AddToRoleAsync(user, model.Roles);
                }

                TempData["SuccessMessage"] = "User updated successfully!";
                return RedirectToAction("List");
            }

            ViewBag.RoleList = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View("EditUser", model);
        }

    }
}


