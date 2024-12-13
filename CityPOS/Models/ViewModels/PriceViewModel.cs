namespace CityPOS.Models.ViewModels
{
    public class PriceViewModel
    {
        public string id { get; set; }
        public string Itemid { get; set; }
        public string  ItemName { get; set; }
        public string Unitid { get; set; }
        public string UnitName { get; set; }
        public DateTime PricingDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
