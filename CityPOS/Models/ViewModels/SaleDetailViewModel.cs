namespace CityPOS.Models.ViewModels
{
    public class SaleDetailsViewModel
    {
        public string id { get; set; }


        public string ItemName { get; set; }
        public string Itemid { get; set; }
        public string UnitName { get; set; }
        public string Unitid { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public decimal Amount { get; set; }
        public string Categoryid { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
