using CityPOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleOrderService _saleOrderService;
        private readonly IItemService _itemService;
        private readonly IUnitService _unitService;
        private readonly IPriceService _priceService;
        private readonly ICustomerService _customerService;

        public SaleController(ISaleOrderService saleOrderService,
            IItemService itemService,
            IUnitService unitService,
            IPriceService priceService,
            ICustomerService customerService)
        {
            this._saleOrderService = saleOrderService;
            this._itemService = itemService;
            this._unitService = unitService;
            this._priceService = priceService;
            this._customerService = customerService;
        }
        public IActionResult Entry()
        {
            bindItemData();
            bindUnitData();
            bindCustomerData();
            return View();
        }

        private void bindCustomerData()
        {
            var customer = _customerService.GetAll();
            ViewBag.Customer= customer;
        }

        private void bindItemData()
        {
            var item = _itemService.GetItems();
            ViewBag.Item = item;
        }

        private void bindUnitData()
        {
          var unit=_unitService.GetUnits();
            ViewBag.Unit = unit;
        }
      
        [HttpGet]
        public IActionResult GetItemDetails(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                return Json(null);
            }

            var itemDetails = _priceService.GetItemDetailsByBarcode(barcode);
            return Json(itemDetails);
        }
    }
}
