using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            this._cityService = cityService;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(CityViewModel cityViewModel)
        {
            try
            {
                var isAlreadyExist = _cityService.IsAlreadyExist(cityViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This City  name is already exist in the system.";
                    ViewData["Status"] = false;
                    return View(cityViewModel);
                }
                _cityService.Create(cityViewModel);
                ViewData["info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["info"] = "Error occur when saving data to the system"+e.Message;
                ViewData["status"] = true;
            }
            return View();
        }
         public IActionResult List()
        {
            var citys=_cityService.GetAll();
            return View(citys);
        }
        public IActionResult Edit(string id) 
        {
            var cityView = _cityService.GetById(id);
            return View(cityView);
        }
        [HttpPost]
        public IActionResult Update(CityViewModel cityViewModel)
        {
            try
            {
                _cityService.Update(cityViewModel);
                TempData["info"] = "Successfully update data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["info"] = "Error occur when updating data to the system" + e.Message;
                TempData["status"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string id)
        {
            try
            {
                _cityService.Delete(id);
                TempData["info"] = "Successfully delete data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["info"] = "Error occur when removing data from the system" + e.Message;
                TempData["status"] = true;
            }
            return RedirectToAction("List");
        }
    }
}
