using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
     

        [HttpGet]
        public IActionResult Entry()
        {

            // Get the next category code
            string nextCode = _categoryService.GetNextCategoryCode();

            // Create a new ViewModel and set the code
            var categoryViewModel = new CategoryViewModel
            {
                Code = nextCode,
                Name = string.Empty,       // Initialize to prevent null reference
                Description = string.Empty  // Initialize to prevent null reference
            };

            ViewData["Info"] = null;
            ViewData["Status"] = null;

            return View(categoryViewModel);

        }
        [HttpPost]
        public IActionResult Entry(CategoryViewModel categoryViewModel)
        {
            try
            {
               
                var isAlreadyExist = _categoryService.IsAlreadyExist(categoryViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This Category code or name is already exist in the system.";
                    ViewData["Status"] = false;
                    return View(categoryViewModel);
                }
                _categoryService.Create(categoryViewModel);
            
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
                return View();
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system"+e.Message;
                ViewData["status"] = false;
             
                return View(categoryViewModel);

            }
            
        }
        public IActionResult List()
        { 
            
          var categories= _categoryService.GetAll();
            return View(categories);
        }
        public IActionResult Edit(string id)
        {
            var categoryView=_categoryService.GetById(id);
            return View(categoryView);
        }
        [HttpPost]
        public IActionResult Update(CategoryViewModel categoryViewModel)
        {
            try
            {

               _categoryService.Update(categoryViewModel);
                TempData["Info"] = "Successfully update data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when updateing data to the system" + e.Message;
                TempData["status"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string id)
        {
            try
            {        _categoryService.Delete(id);
                    TempData["Info"] = "Successfully delete data to the system";
                    TempData["status"] = true;
                
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when deleting data to the system" + e.Message;
                TempData["status"] = false;
            }
            return RedirectToAction("List");
        }
    }
}
