using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("SaleDetail")]
    public class SaleDetatilEntity:BaseEntity
    {
       
        public string Orderid { get; set; }
        public string Itemid { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice {  get; set; }
        public int Quantity { get; set; }
        public string Unitid { get; set; }
        public decimal ActualSaleAmount { get; set; }
        public decimal DisAmount { get; set; }
        public bool IsFOC { get; set; }
        public decimal DisPercent { get; set; }
    }
}
