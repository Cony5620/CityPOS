using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class StockLedgerController : Controller
    {
        private readonly IStockLedgerService _StockLedgerService;
       
        private readonly IItemService _itemService;
        
        private readonly IUnitService _unitService;
       

        public StockLedgerController(IStockLedgerService StockLedgerService,IItemService itemService ,IUnitService unitService)
        {
            this._StockLedgerService = StockLedgerService;
            
            this._itemService = itemService;
            this._unitService = unitService;
            
        }
     
        [HttpGet ]
        public IActionResult Entry()
        {
         
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

        public IActionResult List(DateTime? fromDate, DateTime? toDate, string? itemid)
        {
            var StockLedger = _StockLedgerService.GetAll(fromDate, toDate, itemid);
            bindItemData();
            bindUnitData();
            return View(StockLedger); 
        }
    
    }
}
