using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface ISaleOrderService
    { void Create(SaleWithDetailViewModel model);
        IEnumerable<SaleOrderViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? Customerid = null);
    }
}
