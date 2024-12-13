using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Domain;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            this._unitService = unitService;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(UnitViewModel unitViewModel)
        {
            try
            {
                var isAlreadyExist = _unitService.IsAlreadyExist(unitViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This Unit name is already exist in the system.";
                    ViewData["Status"] = false;
                  
                    return View(unitViewModel);
                }
                _unitService.Create(unitViewModel);
                ViewData["info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = true;
            }
            return View();
        }
        public IActionResult List()
        {var units=_unitService.GetAll();
            return View(units);

        }
        public IActionResult Edit(string id)
        {var unitView=_unitService.GetBy(id);
            
            return View(unitView);
        }
        [HttpPost]
        public IActionResult Update(UnitViewModel unitViewModel)
        {
            try
            {   _unitService.Update(unitViewModel);
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
                    _unitService.Delete(id);
                TempData["info"] = "Successfully delet data to the system";
                    TempData["status"] = true;
                
            }
            catch (Exception e)
            {

                TempData["info"] = "Error occur delet data to the system";
                TempData["status"] = true;
            }
            return RedirectToAction("List");
        }
    }
}
