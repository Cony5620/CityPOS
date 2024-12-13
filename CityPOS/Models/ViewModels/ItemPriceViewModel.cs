namespace CityPOS.Models.ViewModels
{
    public class ItemPriceViewModel
    {
        public string ItemName { get; set; }
        public string Unitid { get; set; }
        public string UnitName { get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal RetailSalePrice { get; set; }
    }
}
