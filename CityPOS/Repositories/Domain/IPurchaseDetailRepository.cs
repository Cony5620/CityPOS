using CityPOS.Models.DataModels;

namespace CityPOS.Repositories.Domain
{
    public interface IPurchaseDetailRepository:IBaseRepository<PurchaseDetailEntity>
    {IEnumerable<PurchaseDetailEntity>FindByPurchaseId(string purchaseid);
        void DeleteRange(IEnumerable<PurchaseDetailEntity> purchaseDetails);
    }
}
