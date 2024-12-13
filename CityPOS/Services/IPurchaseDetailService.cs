using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IPurchaseDetailService
    {
      void Create(PurchaseDetailViewModel purchaseDetailViewModel);
    }
}
