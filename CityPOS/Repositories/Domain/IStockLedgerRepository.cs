using CityPOS.Models.DataModels;

namespace CityPOS.Repositories.Domain
{
    public interface IStockLedgerRepository : IBaseRepository<StockLedgerEntity>
    { IEnumerable<StockLedgerEntity> FindBySourceId(string sourceid); 
        void DeleteRange(IEnumerable<StockLedgerEntity> stockLedgers);
    }
}
