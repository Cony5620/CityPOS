namespace CityPOS.Models.ViewModels
{
    public class SaleDetailReoprtViewModel
    {
        public string SaleDate { get; set; }
        public string VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public string SaleType { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Amount { get; set; }
        public string IsFOC { get; set; }
        public decimal DisPercent { get; set; }
        public decimal DisAmount { get; set; }


    }
}
