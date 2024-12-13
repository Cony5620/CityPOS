using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class PurchaseDetailRepository : BaseRepository<PurchaseDetailEntity>,IPurchaseDetailRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public PurchaseDetailRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void DeleteRange(IEnumerable<PurchaseDetailEntity> purchaseDetails)
        {
            _dbContext.PurchaseDetails.RemoveRange(purchaseDetails);
        }

        public IEnumerable<PurchaseDetailEntity> FindByPurchaseId(string purchaseid)
        {
           return _dbContext.PurchaseDetails.Where(pd=>pd.Purchaseid==purchaseid).ToList();
        }
    }
}
