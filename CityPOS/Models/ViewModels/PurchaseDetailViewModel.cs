namespace CityPOS.Models.ViewModels
{
    public class PurchaseDetailViewModel
    {
        public string id { get; set; }


        public string ItemName { get; set; }
        public string Itemid { get; set; }
        public string UnitName { get; set; }
        public string Unitid { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime ExpiredDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
