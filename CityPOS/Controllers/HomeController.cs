using CityPOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityPOS.DAO;
using System.Linq;
using System;
using System.Diagnostics;
using CityPOS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CityPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CityPOSDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, CityPOSDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                // Retrieve sales data from the database (or your data source)
                var salesData = _dbContext.SaleOrders // Assuming your data source is '_context.Sales'
                    .Select(s => new SaleViewModel
                    {
                        SaleDate = s.SaleDate, // Assuming 'SaleDate' is the actual property
                        CashAmount = s.CashAmount
                    })
                    .ToList();

                // Group sales data by week and calculate total cash amount per week
                var groupedSalesData = salesData
                    .GroupBy(s => new { s.WeekStart, s.WeekEnd }) // Assuming 'WeekStart' and 'WeekEnd' are DateTime fields
                    .Select(g => new
                    {
                        WeekRange = $"{g.Key.WeekStart.ToString("MM/dd/yyyy")} - {g.Key.WeekEnd.ToString("MM/dd/yyyy")}",
                        TotalCashAmount = g.Sum(s => s.CashAmount)
                    })
                    .ToList();

                // Prepare the data for the chart
                var chartData = new
                {
                    Labels = groupedSalesData.Select(sd => sd.WeekRange).ToArray(), // Week ranges for x-axis labels
                    Data = groupedSalesData.Select(sd => sd.TotalCashAmount).ToArray() // Total cash amounts for y-axis
                };

                ViewData["chartData"] = chartData; // Passing chartData to the view

                return View(salesData);  // Pass the model to the view (sales data)
            }

            return View();
        }

            public IActionResult Privacy()
        {
            return View();
        }

        // Error action method to display error pages
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
