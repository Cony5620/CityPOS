using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class TownshipController : Controller
    {
        private readonly ITownshipService _townshipService;
        private readonly ICityService _cityService;

        public TownshipController(ITownshipService townshipService,ICityService cityService)
        {
            this._townshipService = townshipService;
            this._cityService = cityService;
        }
        public IActionResult Entry()
        {
            bindCitydata();
            return View();
        }

        private void bindCitydata()
        {
            var Citys=_cityService.GetCities();
            ViewBag.City = Citys;
        }

        [HttpPost]  
        public IActionResult Entry(TownshipViewModel townshipViewModel)
        {
            try
            {
                var isAlreadyExist = _townshipService.IsAlreadyExist(townshipViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This township  name is already exist in the system.";
                    ViewData["Status"] = false;
                    bindCitydata();
                    return View(townshipViewModel);
                }
                _townshipService.Create(townshipViewModel);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;
            }
            bindCitydata();
            return View();
        }
        public IActionResult List()
        {
            var townships=_townshipService.GetAll();
            return View(townships);
        }
        public IActionResult Edit(string id)
        {

            var townshipsView = _townshipService.GetById(id);
            bindCitydata();
            return View(townshipsView);
        }
        [HttpPost]
        public IActionResult Update(TownshipViewModel townshipViewModel)
        {
            try
            {
                _townshipService.Update(townshipViewModel);
                TempData["Info"] = "Successfully save data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when saving data to the system" + e.Message;
                TempData["status"] = false;
            }
           
            return RedirectToAction("list");
        }
        public ActionResult Delete(string id) 
        {
            try
            {
                _townshipService.Delete(id);
                TempData["Info"] = "Successfully save data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when saving data to the system" + e.Message;
                TempData["status"] = false;
            }

            return RedirectToAction("list");
        }
    }
}
