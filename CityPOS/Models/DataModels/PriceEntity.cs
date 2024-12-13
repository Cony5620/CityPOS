using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Price")]
    public class PriceEntity:BaseEntity
    {
        public string Itemid { get; set; }
        public string Unitid { get; set; }
        public DateTime PricingDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
        public decimal WholeSalePrice { get; set; }

    }
}
