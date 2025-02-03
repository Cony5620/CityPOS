using CityPOS.Models.ViewModels;
using CityPOS.Services.ReportingServices;
using CityPOS.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CityPOS.Controllers
{
    public class ProfitReportController : Controller
    {
        private readonly IProfitReportService _profitReportService;

        public ProfitReportController(IProfitReportService profitReportService)
        {
            this._profitReportService = profitReportService;
        }
        public IActionResult ProfitReport()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProfitReport(string FromDate,string ToDate)
        {

          IList<ProfitReportViewModel> profitReport = _profitReportService.GetProfitReport(FromDate, ToDate);
            string fileName = $"ProfitReport_{DateTime.Now:yyyyMMdd}.xlsx";

            if (profitReport.Any())
            {
                var totalQuantitySold = profitReport.Sum(r => r.QuantitySold);
                var OriginalSellingPrice = profitReport.Sum(r => r.OriginalSellingPrice);
                var discountpercent = profitReport.Sum(r=>r.DiscountPercentage);
                var discountamount = profitReport.Sum(r => r.DiscountAmount);
                var totalPurchasePrice = profitReport.Sum(r => r.PurchasePrice );
                var totalCost = profitReport.Sum(r => r.TotalCost);
                var totalNetSale = profitReport.Sum(r => r.NetSalePrice );
                var totalRevenue = profitReport.Sum(r => r.TotalRevenue);
                var totalProfit = profitReport.Sum(r => r.Profit);
                var averageProfitMargin = profitReport.Average(r => r.ProfitMargin);
                profitReport.Add(new ProfitReportViewModel
                {
                    CategoryName = "Total",
                    QuantitySold = totalQuantitySold,
                    OriginalSellingPrice = OriginalSellingPrice,
                    DiscountPercentage = discountpercent,
                    DiscountAmount = discountamount,
                    PurchasePrice = totalPurchasePrice,
                    TotalCost = totalCost,
                    NetSalePrice = totalNetSale,
                    TotalRevenue = totalRevenue,
                    Profit = totalProfit,
                    ProfitMargin = averageProfitMargin

                });
                var fileContentsInBytes = ReportHelper.ExportToExcel(profitReport, fileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreadsheet.sheet";
                ViewData["Info"] = "Successfully download the overdue ExcelFile.";
                ViewData["Status"] = true;
                return File(fileContentsInBytes, contentType, fileName);
            }
            else
            {
                ViewData["Info"] = "not profit report.";
                ViewData["Status"] = false;
                return View();
            }
           
          }
    }
}
