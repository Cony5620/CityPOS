namespace CityPOS.Models.ViewModels
{
    public class StockLedgerViewModel
    {
        public string id { get; set; }
       
        public DateTime? LedgerDate { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public string ItemCode { get; set; }
        public string Itemid { get; set; }
        public string Unitid { get; set; }
        public string TransactionType { get; set; }
        public int InQty { get; set; }
        public int OutQty { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
