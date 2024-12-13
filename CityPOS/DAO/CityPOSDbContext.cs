using CityPOS.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.DAO
{
    public class CityPOSDbContext:IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public CityPOSDbContext(DbContextOptions<CityPOSDbContext>options):base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<UnitEntity> Units { get; set; }
        public DbSet<CityEntity> Citys { get; set; }    
        public DbSet<TownshipEntity> Townships { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ItemEntity>Items { get; set; }
        public DbSet<PriceEntity> Prices { get; set; }
        public DbSet<StockBalanceEntity> StockBalances { get; set; }
        public DbSet<StockLedgerEntity> StockLedgers { get; set; }
        public DbSet<PurchaseDetailEntity> PurchaseDetails { get; set; }
        public DbSet<PurchaseEntity>Purchases { get; set; } 
        public DbSet<SaleOrderEntity>SaleOrders { get; set; }
        public DbSet<SaleDetatilEntity> SaleDetatils { get; set; }
    }
}
