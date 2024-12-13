using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IStockBalanceService
    {
        void CreateOrUpdate(StockBalanceViewModel stockBalanceViewModel);
        IEnumerable<StockBalanceViewModel> GetAll(string categoryid,string itemid);


    }
}
