using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IPurchaseService
    {
        void Create(PurchaseWithDetailViewModel models);
        IEnumerable<PurchaseViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? Supplierid = null,string purchaseid=null);
        IEnumerable<PurchaseViewModel> GetPurchases();
        bool Delete(string id);
      
    }
}
