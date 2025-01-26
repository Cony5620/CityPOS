namespace CityPOS.Models.ViewModels
{
    public class SaleViewModel
    {
        public string Week { get; set; }
        public decimal CashAmount { get; set; }

        // Change SaleDate to DateTime type
        public DateTime SaleDate { get; set; }

        public Dictionary<string, decimal> DailySales { get; set; }

        // WeekStart Property
        public DateTime WeekStart
        {
            get
            {
                // Get the DayOfWeek value for SaleDate
                var diff = SaleDate.DayOfWeek - DayOfWeek.Monday;

                // If the SaleDate is Sunday, adjust by adding 7 to make the calculation work
                if (diff < 0) diff += 7;

                // Return the start of the week (Monday)
                return SaleDate.AddDays(-diff).Date;
            }
        }

        // WeekEnd Property
        public DateTime WeekEnd
        {
            get
            {
                // Week end is 6 days after the week start (Sunday)
                return WeekStart.AddDays(6);
            }
        }
    }
}
