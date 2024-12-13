using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("StockLedger")]
    public class StockLedgerEntity:BaseEntity
    {
        public DateTime LedgerDate { get; set; }
        public string Itemid { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Sourceid { get; set; }
        public string transactionType { get; set; }
      
        public int Quantity { get; set; }
        public string Unitid { get; set; }
      
    }
}
