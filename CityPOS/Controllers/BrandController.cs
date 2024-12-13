using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public BrandController(IBrandService brandService,ICategoryService categoryService)
        {
            this._brandService = brandService;
            this._categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Entry()
        {
            string nextCode = _brandService.GetLastBrandCode();

            var brandViewModel = new BrandViewModel
            {
                Code = nextCode,
                Name = string.Empty,
                Categoryid = string.Empty,
                CategoryName = string.Empty,
            };

            ViewData["Info"] = null;
            ViewData["Status"] = null;
          
            bindCategoryData();
            return View(brandViewModel);
        }

        private void bindCategoryData()
        {
            var categories=_categoryService.GetCategories();
            ViewBag.Categories= categories;
        }

        [HttpPost]
        public IActionResult Entry(BrandViewModel brandViewModel)
        {
            try
            {
                var isAlreadyExist = _brandService.IsAlreadyExist(brandViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This Brand code or name is already exist in the system.";
                    ViewData["Status"] = false;
                    bindCategoryData();
                    return View(brandViewModel);
                }
                _brandService.Create(brandViewModel);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;
            }
            bindCategoryData();
            return View();
        }
        public IActionResult List()
        {
           
            var brands= _brandService.GetAll();
            return View(brands);
        }
        public IActionResult Edit(string id)
        {var brandView=_brandService.GetById(id);
           
            bindCategoryData();
            return View(brandView);
        }
        [HttpPost]
        public IActionResult Update(BrandViewModel brandViewModel)
        {
            try
            {

                _brandService.Update(brandViewModel);
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
            {
                _brandService.Delete(id);
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
