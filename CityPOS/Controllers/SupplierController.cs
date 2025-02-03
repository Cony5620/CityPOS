using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _SupplierService;
        private readonly ICityService _cityService;
        private readonly ITownshipService _townshipService;

        public SupplierController(ISupplierService SupplierService,ICityService cityService,ITownshipService townshipService)
        {
            this._SupplierService = SupplierService;
            this._cityService = cityService;
            this._townshipService = townshipService;
        }
        [HttpGet]
        public IActionResult GetTownships(string cityid)
        {
         

            var townships = _townshipService.GetTownshipsByCityId(cityid); // Use cityId.Value if it's nullable
            return Json(townships);
        }


        [HttpGet]
        public IActionResult Entry()
        {
            string nextCode = _SupplierService.GetNextSupplierCode();

            var supplierViewModel = new SupplierViewModel
            {
                Code = nextCode,
                Name = string.Empty,
                Email = string.Empty,
                Adress = string.Empty,  
                Cityid = string.Empty,
                CityName= string.Empty,
                Townshipid= string.Empty,
                TownshipName= string.Empty,
                Company= string.Empty,
                PhoneNo= string.Empty,

               
            };

            ViewData["Info"] = null;
            ViewData["Status"] = null;
            bindCityData();
            bindTownshipData();
            return View(supplierViewModel);
        }

        private void bindTownshipData()
        {
           var townships=_townshipService.GetTownships();
            ViewBag.Townships = townships;
        }

        private void bindCityData()
        {
           var citys=_cityService.GetCities();
            ViewBag.Citys = citys;  
        }

        [HttpPost]
        public IActionResult Entry(SupplierViewModel SupplierViewModel)
        {
            try
            {
                var IsAlreadyExist =_SupplierService.IsAlreadyExist(SupplierViewModel);
                if (IsAlreadyExist)
                {
                    ViewData["Info"] = "This Supplier code or name is already exist in the system.";
                    ViewData["status"] = false;
                    bindCityData();
                    bindTownshipData();
                    return View(SupplierViewModel);
                }
                _SupplierService.Create(SupplierViewModel);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;
            }
            bindCityData();
            bindTownshipData();
            return View();
        }
        public IActionResult List()
        {
           
            var Suppliers= _SupplierService.GetAll();
            return View(Suppliers);
        }
        public IActionResult Edit(string id)
        {var SupplierView=_SupplierService.GetById(id);

            bindCityData();
            bindTownshipData();
            return View(SupplierView);
        }
        [HttpPost]
        public IActionResult Update(SupplierViewModel SupplierViewModel)
        {
            try
            {

                _SupplierService.Update(SupplierViewModel);
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
                _SupplierService.Delete(id);
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
