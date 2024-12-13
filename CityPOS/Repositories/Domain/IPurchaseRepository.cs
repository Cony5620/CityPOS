using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IPurchaseRepository:IBaseRepository<PurchaseEntity>
    {
        IEnumerable<PurchaseViewModel> GetPurchases();
        PurchaseEntity FindById(string id);
    }
}
