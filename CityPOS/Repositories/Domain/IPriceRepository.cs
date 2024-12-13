using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IPriceRepository:IBaseRepository<PriceEntity>
    {
        IEnumerable<PriceViewModel> GetPrices();
       
        ItemPriceViewModel GetItemDetailsByBarcode(string barcode);
    }
}
