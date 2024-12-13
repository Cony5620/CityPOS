using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IStockLedgerService
    {
        IEnumerable<StockLedgerViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? itemid = null);
    }
}
