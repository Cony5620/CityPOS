using CityPOS.Models; // Import the SaleViewModel
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Diagnostics;
using CityPOS.DAO;
using System.Globalization;
using CityPOS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

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
                var salesData = _dbContext.SaleOrders
                .Where(s => s.SaleDate >= DateTime.Now.AddMonths(-3))
                .ToList();


                var weeklySalesData = salesData
                    .GroupBy(s => new
                    {
                        Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(s.SaleDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                        Year = s.SaleDate.Year
                    })
                    .Select(g => new SaleViewModel
                    {
                        Week = $"Week {g.Key.Week} ({g.Key.Year})",
                        SalesAmount = g.Sum(s => s.TotalAmount)
                    })
                    .OrderBy(x => x.Week)
                    .ToList();


                return View(weeklySalesData);
            }
            return View(null);
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
