using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class StockBalanceRepository : BaseRepository<StockBalanceEntity>,IStockBalanceRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public StockBalanceRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

  
        public StockBalanceEntity GetStockBalanceByItemUnitAndExpiration(string itemid, string unitid, DateTime expirationDate)
        {
            return _dbContext.StockBalances
                            .FirstOrDefault(sb =>
                                sb.Itemid == itemid &&
                                sb.Unitid == unitid &&
                                sb.ExpiredDate.Date == expirationDate.Date); ;
        }

        public void UpdateStockBalanceByItemUnitAndExpiration(string itemid, string unitid, DateTime expirationDate,int Quantity, string Categoryid )
        {
            var stockBalanceEntity = _dbContext.StockBalances
                .FirstOrDefault(sb =>
                    sb.Itemid == itemid &&
                    sb.Unitid == unitid &&
                    sb.ExpiredDate.Date == expirationDate.Date);

            if (stockBalanceEntity != null)
            {
              
                stockBalanceEntity.Quantity += Quantity;
                if (stockBalanceEntity.Quantity < 0)
                {
                    throw new InvalidOperationException($"Insufficient stock for Item: {itemid} and Unit: {unitid} with Expiration: {expirationDate}. Cannot reduce stock below zero.");
                }
                _dbContext.StockBalances.Update(stockBalanceEntity);
            }
            else
            {
                if (Quantity < 0)
                {
                    // Trying to decrease stock for a non-existing entry
                    throw new InvalidOperationException($"No stock balance found for Item: {itemid}, Unit: {unitid}, Expiration: {expirationDate} to reduce by {Math.Abs(Quantity)}.");
                }

                stockBalanceEntity = new StockBalanceEntity
                {
                    id = Guid.NewGuid().ToString(),
                   Categoryid=Categoryid,
                    Itemid = itemid,
                    Unitid = unitid,
                    Quantity = Quantity,
                    ExpiredDate = expirationDate,
                    CreatedAt = DateTime.UtcNow
                };
                _dbContext.StockBalances.Add(stockBalanceEntity);
            }

            _dbContext.SaveChanges();

        }
    }
}
