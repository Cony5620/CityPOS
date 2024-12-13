namespace CityPOS.Models.ViewModels
{
    public class SupplierViewModel
    {
        public string id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cityid { get; set; }
        public string CityName { get; set; }
        public string? Adress { get; set; }
        public string Townshipid { get; set; }
        public string TownshipName { get; set; }
        public string PhoneNo { get; set; }
        public string Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
