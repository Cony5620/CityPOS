namespace CityPOS.Models.ViewModels
{
    public class PurchaseWithDetailViewModel
    {
        public PurchaseViewModel Purchase { get; set; }
        public List<ItemDetailViewModel> ItemDetails { get; set; }

       
        public PurchaseWithDetailViewModel()
        {
            Purchase = new PurchaseViewModel(); 
            ItemDetails = new List<ItemDetailViewModel>(); 
        }
    }

    public class PurchaseViewModel
    {
        public string id { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Today;
        public string PurchaseVoNo { get; set; }
        public string Supplierid { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CashAmount { get; set; }


        public PurchaseViewModel() { }
    }

    public class ItemDetailViewModel
    {
        public string Itemid { get; set; }
        public string Unitid { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ExpiredDate { get; set; }

        // Parameterless constructor
        public ItemDetailViewModel() { }
    }
}
