using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CityPOS.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: User/Create
        public IActionResult Create()
        {
            // Fetch roles from RoleManager and pass to the view using ViewBag
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles; // Use ViewBag to pass roles to the view

            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AspNetUsersViewModel model)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Create a new IdentityUser instance
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                // Create the user asynchronously
                var result = await _userManager.CreateAsync(user, model.Password);

                // If user creation succeeds, assign role and redirect
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    }

                    // Success message
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Index", "Home");
                }

                // If creation failed, add errors to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If model is invalid or there are validation errors, reload roles for the dropdown
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.Roles = roles; // Reload roles in case of validation errors

            return View(model);
        }
    }
}
