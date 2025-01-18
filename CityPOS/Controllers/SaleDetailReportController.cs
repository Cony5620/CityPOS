using CityPOS.Models.ViewModels;
using CityPOS.Services;
using CityPOS.Services.ReportingServices;
using CityPOS.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class SaleDetailReportController : Controller
    {
        private readonly ISaleDetailReportService _saleDetailReportService;
        private readonly IItemService _itemService;

        public SaleDetailReportController(ISaleDetailReportService saleDetailReportService,IItemService itemService)
        {
            this._saleDetailReportService = saleDetailReportService;
            this._itemService = itemService;
        }



        public IActionResult SaleDetailReport()
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
        public IActionResult SaleDetailReport(string fromDate,string ToDate,string itemid)
        {
            IList<SaleDetailReoprtViewModel>SaleDetails=_saleDetailReportService.SaleDetails(fromDate, ToDate, itemid);
            string fileName = $"SaleDetailReport{DateTime.Now.ToString()}.xlsx";
            if (SaleDetails.Any())
            { 
                var totalQuantity = SaleDetails.Sum(s => s.Quantity);
                var totalPrice = SaleDetails.Sum(s => s.Price);
                var totalAmount = SaleDetails.Sum(s => s.Amount);
                var totalTotal = SaleDetails.Sum(s => s.Total);

               
               SaleDetails.Add(new SaleDetailReoprtViewModel
                {
                    UnitName = "Total",
                   
                    Quantity = totalQuantity,
                    Price = totalPrice,
                    Amount = totalAmount,
                    Total = totalTotal
                });

               

                var fileContentInBytes =ReportHelper.ExportToExcel(SaleDetails,fileName) ;
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the saledetail ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentInBytes, contentType, fileName);
            }
            else
            {

                BindItemData();
                ViewData["Info"] = "Not saledetail";
                ViewData["Status"] = false;
                return View();
            }
           
        }
    }
}
