using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class AspNetRolesController : Controller
    {
        private readonly IAspNetRolesService _aspNetRolesService;

        public AspNetRolesController(IAspNetRolesService aspNetRolesService)
        {
            this._aspNetRolesService = aspNetRolesService;
        }
      
        [HttpGet]
        public async Task<IActionResult> AspNetRoles()
        {
            var roles = await _aspNetRolesService.GetAllAsync();
            ViewData["Roles"] = roles;
            return View(new AspNetRolesViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AspNetRoles(AspNetRolesViewModel aspNetRolesViewModel)
        {
            try
            {
                // Use CreateAsync instead of Create
                await _aspNetRolesService.CreateAsync(aspNetRolesViewModel);
                ViewData["Info"] = "Successfully saved data to the system";
                ViewData["status"] = true;
               
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occurred when saving data to the system: " + e.Message;
                ViewData["status"] = false;
            }
            var roles = await _aspNetRolesService.GetAllAsync();
            ViewData["Roles"] = roles;
            return View(new AspNetRolesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _aspNetRolesService.DeleteAsync(id);
                TempData["Info"] = "Role successfully deleted.";
                TempData["status"] = true;
            }
            catch (Exception ex)
            {
                TempData["Info"] = $"Error deleting role: {ex.Message}";
                TempData["status"] = false;
            }

            return RedirectToAction("AspNetRoles");
        }
    }
}
