using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IPriceService
    { void Create(PriceViewModel PriceViewModel);
        IEnumerable<PriceViewModel> GetAll();
        PriceViewModel GetById(string id);
        void Update(PriceViewModel PriceViewModel);
        bool Delete(string id);
        IEnumerable<PriceViewModel>GetPrices();
      
        ItemPriceViewModel GetItemDetailsByBarcode(string barcode);
    }
}
