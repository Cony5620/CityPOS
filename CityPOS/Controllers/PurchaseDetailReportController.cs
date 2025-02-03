using CityPOS.Models.ViewModels;
using CityPOS.Services;
using CityPOS.Services.ReportingServices;
using CityPOS.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class PurchaseDetailReportController : Controller
    {
        private readonly IPurchaseDetailReportService _purchaseDetailReportService;
        private readonly IItemService _itemService;

        public PurchaseDetailReportController(IPurchaseDetailReportService purchaseDetailReportService,
            IItemService itemService)
        {
            this._purchaseDetailReportService = purchaseDetailReportService;
            this._itemService = itemService;
        }
        public IActionResult PurchaseDetailReport()
        {
            BindItemData();
            return View();
        }

        private void BindItemData()
        {
            var item = _itemService.GetItems();
            ViewBag.Items = item;
        }
        [HttpPost]
        public IActionResult PurchaseDetailReport(string FromDate, string ToDate, string itemid)
        {
            IList<PurchaseDetailReportViewModel> PurchaseDetails=_purchaseDetailReportService.PurchaseDetails( FromDate,  ToDate,  itemid);
            string fileName = $"PurchaseDetailReport{DateTime.Now.ToString()}.xlsx";
            if (PurchaseDetails.Any())
            {
                var totalQuantity = PurchaseDetails.Sum(pd => pd.Quantity);
                var totalPrice = PurchaseDetails.Sum(pd => pd.Price);
                var totalAmount = PurchaseDetails.Sum(pd => pd.TotalPrice);

               
                PurchaseDetails.Add(new PurchaseDetailReportViewModel
                {
                    UnitName = "TOTAL",
                    Quantity = totalQuantity,
                    Price = totalPrice,
                    TotalPrice = totalAmount
                });
                var fileContentsInBytes = ReportHelper.ExportToExcel(PurchaseDetails, fileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the purchasedeatil ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentsInBytes, contentType, fileName);
            }
            else
            {

                BindItemData();
                ViewData["Info"] = "No Purchase Details found.";
                ViewData["Status"] = false;
                return View();
            }

        }

    }
}
