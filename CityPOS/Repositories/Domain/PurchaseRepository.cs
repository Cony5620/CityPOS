using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class PurchaseRepository : BaseRepository<PurchaseEntity>,IPurchaseRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public PurchaseRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public PurchaseEntity FindById(string id)
        {
           return _dbContext.Purchases.FirstOrDefault(p=>p.id==id);
        }

        public IEnumerable<PurchaseViewModel> GetPurchases()
        {
           return _dbContext.Purchases.Where(w=>!w.IsInActive).Select(s=>new PurchaseViewModel
           {
               id=s.id,
               PurchaseVoNo=s.PurchaseVoNO,
           }).ToList();
        }
    }
}
