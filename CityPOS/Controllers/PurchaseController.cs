using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace CityPOS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IItemService _itemService;
        
        private readonly IUnitService _unitService;
        private readonly ISupplierService _supplierService;
        private readonly IPurchaseDetailService _purchaseDetailService;

        public PurchaseController(IPurchaseService purchaseService,IItemService itemService ,IUnitService unitService,ISupplierService supplierService,IPurchaseDetailService purchaseDetailService)
        {
            this._purchaseService = purchaseService;
            this._itemService = itemService;
            this._unitService = unitService;
            this._supplierService = supplierService;
            this._purchaseDetailService = purchaseDetailService;
        }
     
        [HttpGet ]
        public IActionResult Entry()
        {
            bindPurchaseData();
            bindSupplierData();
            bindItemData();
            bindUnitData();
            var model = new PurchaseWithDetailViewModel();
            return View(model);
        }

        private void bindPurchaseData()
        {
           var purchase=_purchaseService.GetPurchases();
            ViewBag.Purchase = purchase;
        }

        private void bindSupplierData()
        {
           var supplier=_supplierService.GetSuppliers();
            ViewBag.supplier = supplier;
        }

        private void bindItemData()
        {
            var item = _itemService.GetItems();
            ViewBag.item = item;
        }

        private void bindUnitData()
        {
            var unit = _unitService.GetUnits();
            ViewBag.Unit = unit;    
        }

        [HttpPost]
        public IActionResult Entry(PurchaseWithDetailViewModel model)
        {
            try
            {
                
               
                _purchaseService.Create(model);

                ViewData["Info"] = "Successfully saved data to the system.";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occurred while saving data to the system: " + e.Message;
                ViewData["status"] = false;
            }

           
            bindSupplierData();
            bindItemData();
            bindUnitData();

            return View(model); 
        }
        [HttpGet]
      public IActionResult List(DateTime? fromDate = null, DateTime? toDate = null, string? Supplierid = null, string purchaseid = null)
        {
          
           
            var purchase=_purchaseService.GetAll(  fromDate  ,  toDate ,  Supplierid,  purchaseid );
            bindSupplierData();
            bindPurchaseData();
            return View(purchase);
        }
        public IActionResult Delete(string id)
        {
            try
            {
                _purchaseService.Delete(id);

                TempData["Info"] = "Successfully delete data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when deleting data to the system" + e.Message;
                TempData["status"] = false;
            }
            bindSupplierData();
            bindPurchaseData();
            return RedirectToAction("list");
        }
    }
}
