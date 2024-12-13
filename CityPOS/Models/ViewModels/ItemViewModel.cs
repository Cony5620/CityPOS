namespace CityPOS.Models.ViewModels
{
    public class ItemViewModel
    {
        public string id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Categoryid { get; set; }
        public string CategoryName { get; set; }
        public string Brandid { get; set; }
        public string BrandName { get; set; }
        public string? photo { get; set; }
        public IFormFile? photoimg { get; set; }
        public string Supplierid { get; set; }
        public string SupplierName { get; set; }
        public string BarCode { get; set; }
        public string FDANo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
