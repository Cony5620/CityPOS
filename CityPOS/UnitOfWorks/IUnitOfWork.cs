using CityPOS.Repositories.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace CityPOS.UnitOfWorks
{
    public interface IUnitOfWork
    {//Register the repositories
        ICategoryRepository CategoryRepository { get; }
        IUnitRepository UnitRepository { get; }
        IBrandRepository BrandRepository { get; }
        ICityRepository CityRepository { get; }
        ITownshipRepository TownshipRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IItemRepository ItemRepository { get; }
        IPriceRepository PriceRepository { get; }
        IStockBalanceRepository StockBalanceRepository { get; }
        IStockLedgerRepository StockLedgerRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        IPurchaseDetailRepository PurchaseDetailRepository { get; }
        ISaleDetailRepository SaleDetailRepository { get; }
        ISaleOrderRepository SaleOrderRepository { get; }
        //Commit stages (insert,update,delete)
        void Commit();
        //Rollback transcation
        void Rollback();
        IDbContextTransaction BeginTransaction();
    }
}
