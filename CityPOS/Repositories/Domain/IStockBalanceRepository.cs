using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IStockBalanceRepository:IBaseRepository<StockBalanceEntity>
    {
        StockBalanceEntity GetStockBalanceByItemUnitAndExpiration(string itemId, string unitId, DateTime expirationDate);
        void UpdateStockBalanceByItemUnitAndExpiration(string itemid, string unitid, DateTime expirationDate,int Quantity,string Categoryid);
        List<(string ExpiredDate, int QuantityUsed)> ReduceStockForSale(string itemId, string unitId, int quantity);
    }
}
