using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("PurchaseDetail")]
    public class PurchaseDetailEntity:BaseEntity
    {
      
        public string Itemid { get; set; }
        public string Unitid { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int PurchaseQty { get; set; }
        public string Purchaseid { get; set; } 
        // Foreign key to Purchase
        public virtual PurchaseEntity Purchase { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
