namespace CityPOS.Models.ViewModels
{
    public class TownshipViewModel
    {
        public string  id { get; set; }
        public string Name { get; set; }
        public string Cityid { get; set; }
        public string City { get; set; }
        public decimal DeliveryFees { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
