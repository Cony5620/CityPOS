using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class StockBalanceController : Controller
    {
        private readonly IStockBalanceService _StockBalanceService;
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IUnitService _unitService;
       

        public StockBalanceController(IStockBalanceService StockBalanceService,ICategoryService categoryService,IItemService itemService ,IUnitService unitService)
        {
            this._StockBalanceService = StockBalanceService;
            this._categoryService = categoryService;
          
            this._itemService = itemService;
            this._unitService = unitService;
            
        }
        [HttpGet]
        public IActionResult GetItems(string categoryid)
        {


            var items = _itemService.GetItemsByCategory(categoryid); 
            return Json(items);
        }
        [HttpGet ]
        public IActionResult Entry()
        {
            bindCategoryData();
            bindItemData();
            bindUnitData();

            return View();
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

        
        private void bindCategoryData()
        {
           var category=_categoryService.GetCategories();
            ViewBag.Category = category;
        }

        [HttpPost]
        public IActionResult Entry(StockBalanceViewModel StockBalanceViewModel)
        {
            try
            {
                _StockBalanceService.CreateOrUpdate(StockBalanceViewModel);

                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
               
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;


            }
            bindCategoryData();
           
            bindItemData();
            bindUnitData();
            return View();
        }
        public IActionResult List(string? categoryid,string? itemid)
        {var stockbalance= _StockBalanceService.GetAll(categoryid,itemid);
            bindCategoryData();
            bindItemData();
            return View(stockbalance); 
        }
    
    }
}
