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

        public int GetAvailableStock(string itemId, string unitId)
        {
            return _dbContext.StockBalances
           .Where(sb => sb.Itemid == itemId && sb.Unitid == unitId)
           .Sum(sb => sb.Quantity);
        }

        public StockBalanceEntity GetStockBalanceByItemUnitAndExpiration(string itemid, string unitid, DateTime expirationDate)
        {
            return _dbContext.StockBalances
                            .FirstOrDefault(sb =>
                                sb.Itemid == itemid &&
                                sb.Unitid == unitid &&
                                sb.ExpiredDate.Date == expirationDate.Date); ;
        }

        public List<(string ExpiredDate, int QuantityUsed)> ReduceStockForSale(string itemId, string unitId, int quantity)
        {
            // Fetch all stock balances for the given item and unit, ordered by expiration date
                var stockBalances = _dbContext.StockBalances
             .Where(sb => sb.Itemid == itemId && sb.Unitid == unitId)
             .OrderBy(sb => sb.ExpiredDate) // Prioritize by earliest expiration
             .ToList();

            var usedBatches = new List<(string ExpiredDate, int QuantityUsed)>();

            foreach (var stockBalance in stockBalances)
            {
                if (quantity <= 0)
                    break;

                int usedQuantity = 0;

                if (stockBalance.Quantity >= quantity)
                {
                    usedQuantity = quantity;
                    stockBalance.Quantity -= quantity;
                    quantity = 0;
                }
                else
                {
                    usedQuantity = stockBalance.Quantity;
                    quantity -= stockBalance.Quantity;
                    stockBalance.Quantity = 0;
                }

                _dbContext.StockBalances.Update(stockBalance);

                // Add the batch's expiration date and quantity used to the list
                usedBatches.Add((ExpiredDate: stockBalance.ExpiredDate.ToString(), QuantityUsed: usedQuantity));
            }

            if (quantity > 0)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }

            _dbContext.SaveChanges();
            return usedBatches;
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
