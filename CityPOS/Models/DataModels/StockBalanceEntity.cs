using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("StockBalance")]
    public class StockBalanceEntity:BaseEntity
    {
        public string Itemid { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Categoryid { get; set; }
       
        public int Quantity { get; set; }
        public string Unitid { get; set; }
      
    }
}
