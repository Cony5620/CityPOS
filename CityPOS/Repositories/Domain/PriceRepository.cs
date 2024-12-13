using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class PriceRepository : BaseRepository<PriceEntity>,IPriceRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public PriceRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public ItemPriceViewModel GetItemDetailsByBarcode(string barcode)
        {
           
                var itemDetails = (from price in _dbContext.Prices
                                   join item in _dbContext.Items on price.Itemid equals item.id
                                   join unit in _dbContext.Units on price.Unitid equals unit.id
                                   where item.BarCode == barcode && !price.IsInActive
                                   select new ItemPriceViewModel
                                   {
                                       ItemName = item.Name,
                                       Unitid = price.Unitid,
                                       UnitName=unit.Name,
                                       
                                       WholeSalePrice = price.WholeSalePrice,
                                       RetailSalePrice = price.RetailSalePrice
                                   }).FirstOrDefault();

                return itemDetails;
            
        }

        public IEnumerable<PriceViewModel> GetPrices()
        {
            return _dbContext.Prices.Where(w => !w.IsInActive).Select(s => new PriceViewModel
            {
                id = s.id,
                

            }).ToList();
        }

     
    }
}
