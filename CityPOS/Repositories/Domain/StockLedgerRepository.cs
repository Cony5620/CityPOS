using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class StockLedgerRepository : BaseRepository<StockLedgerEntity>,IStockLedgerRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public StockLedgerRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public void DeleteRange(IEnumerable<StockLedgerEntity> stockLedgers)
        {
           _dbContext.StockLedgers.RemoveRange(stockLedgers);
        }

        public IEnumerable<StockLedgerEntity> FindBySourceId(string sourceid)
        {
           return _dbContext.StockLedgers.Where(sl=>sl.Sourceid == sourceid).ToList();
        }
    }
}
