using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class PriceController : Controller
    {
        private readonly IPriceService _priceService;
        private readonly IItemService _itemService;
        private readonly IUnitService _unitService;

        public PriceController(IPriceService priceService,IItemService itemService,IUnitService unitService )
        {
            this._priceService = priceService;
            this._itemService = itemService;
            this._unitService = unitService;
        }

        public IActionResult Entry()
        {
            bindItemData();
            bindUnitData();
        
            return View();
        }

        private void bindItemData()
        {
            var item = _itemService.GetItems();
            ViewBag.Item = item;
        }

        private void bindUnitData()
        {
            var unit = _unitService.GetUnits();
            ViewBag.Unit = unit;
        }

        [HttpPost]
        public IActionResult Entry(PriceViewModel PriceViewModel)
        {
            try
            {
                _priceService.Create(PriceViewModel);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;
            }
            bindItemData();
            bindUnitData();
            return View();
        }
        public IActionResult List()
        {
           
            var Prices= _priceService.GetAll();
            return View(Prices);
        }
        public IActionResult Edit(string id)
        {var PriceView=_priceService.GetById(id);

            bindItemData();
            bindUnitData();
            return View(PriceView);
        }
        [HttpPost]
        public IActionResult Update(PriceViewModel PriceViewModel)
        {
            try
            {

                _priceService.Update(PriceViewModel);
                TempData["Info"] = "Successfully update data to the system";
                ViewData["status"] = true;
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
                _priceService.Delete(id);
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
