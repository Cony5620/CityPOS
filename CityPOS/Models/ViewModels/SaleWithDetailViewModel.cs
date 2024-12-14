namespace CityPOS.Models.ViewModels
{
    public class SaleWithDetailViewModel
    {
        public SaleOrderViewModel SaleOrder { get; set; }
        public List<SaleDetailViewModel> SaleDetails { get; set; }

        public SaleWithDetailViewModel()
        {
            SaleOrder = new SaleOrderViewModel();
            SaleDetails = new List<SaleDetailViewModel>();
        }
    }
    public class SaleOrderViewModel
    {
        public string id { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Today;
        public string VoucherNo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetTotal { get; set; }
        public decimal CashAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Tax { get; set; }
        public decimal Balance { get; set; }
        public string Paymentid { get; set; }
        public string Customerid { get; set; }
        public int Step { get; set; }
        public decimal TotalReturnAmount { get; set; }
        public decimal DeliverFees { get; set; }
        public string Userid { get; set; }
        public decimal DisPercent { get; set; }
        public decimal DisAmount { get; set; }
        public string SaleTime { get; set; } 

        public SaleOrderViewModel() { }
    }
    public class SaleDetailViewModel
    {
        public string SaleOrderid { get; set; }
        public string Itemid { get; set; }
        public string Unitid{ get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFOC { get; set; }
        public decimal DisPercent { get; set; }
        public decimal DisAmount { get; set; }
        public decimal ActualSaleAmount { get; set; }

        public SaleDetailViewModel() { }
    }
}
