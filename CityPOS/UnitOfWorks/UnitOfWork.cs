using CityPOS.DAO;
using CityPOS.Repositories.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace CityPOS.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CityPOSDbContext _dbContext;

        public UnitOfWork(CityPOSDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository = _categoryRepository ?? new CategoryRepository(_dbContext);
            }
        }
        private IUnitRepository _unitRepository;
        public IUnitRepository UnitRepository
        {
            get
            {
                return _unitRepository = _unitRepository ?? new UnitRepository(_dbContext);
            }
        }
        private IBrandRepository _brandRepository;
        public IBrandRepository BrandRepository
        {
            get
            {
                return _brandRepository = _brandRepository ?? new BrandRepository(_dbContext);
            }
        }
        private ICityRepository _cityRepository;
        public ICityRepository CityRepository
        {
            get
            { return _cityRepository = _cityRepository ?? new CityRepository(_dbContext);
            }
        }
        private ITownshipRepository _townshipRepository;
        public ITownshipRepository TownshipRepository
        {
            get
            {
                return _townshipRepository = _townshipRepository ?? new TownshipRepository(_dbContext);
            }
        }
        private ISupplierRepository _supplierRepository;
        public ISupplierRepository SupplierRepository
        {
            get
            {
                return _supplierRepository = _supplierRepository ?? new SupplierRepository(_dbContext);
            }
        }
        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            { return _customerRepository = _customerRepository ?? new CustomerRepository(_dbContext);
            }
        }
        private IItemRepository _itemRepository;
        public IItemRepository ItemRepository
        {
            get
            {
                return _itemRepository = _itemRepository ?? new ItemRepository(_dbContext);
            }
        }
        private IPriceRepository _priceRepository;
        public IPriceRepository PriceRepository
        {
            get
            {
                return _priceRepository = _priceRepository ?? new PriceRepository(_dbContext);
            }
        }
        private IStockBalanceRepository _stockBalanceRepository;
        public IStockBalanceRepository StockBalanceRepository
        {
            get
            {
                return _stockBalanceRepository=_stockBalanceRepository ?? new StockBalanceRepository(_dbContext);
            }
        }
        private IStockLedgerRepository _stockLedgerRepository;
        public IStockLedgerRepository StockLedgerRepository
        {
            get {
                return _stockLedgerRepository = _stockLedgerRepository ?? new StockLedgerRepository(_dbContext);
                }
        }
        private IPurchaseRepository _purchaseRepository;
        public IPurchaseRepository PurchaseRepository
        {
            get
            {
                return _purchaseRepository = _purchaseRepository ?? new PurchaseRepository(_dbContext);
            }
        }
        private IPurchaseDetailRepository _purchaseDetailRepository;
        public IPurchaseDetailRepository PurchaseDetailRepository
        {
            get 
            {
                return _purchaseDetailRepository= _purchaseDetailRepository ?? new PurchaseDetailRepository(_dbContext);    
            }
        }
        private ISaleDetailRepository _saleDetailRepository;
        public ISaleDetailRepository SaleDetailRepository
        {
            get
            {
                return _saleDetailRepository= _saleDetailRepository ?? new SaleDetailRepository(_dbContext); 
            }
        }
        private ISaleOrderRepository _saleOrderRepository;
        public ISaleOrderRepository SaleOrderRepository
        {
            get
            {
                return _saleOrderRepository= _saleOrderRepository ?? new SaleOrderRepository(_dbContext);   
            }
        }
       

        public void Commit()
        {
           _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            _dbContext.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
    }
}
