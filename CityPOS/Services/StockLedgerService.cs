using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace CityPOS.Services
{
    public class StockLedgerService : IStockLedgerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockLedgerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<StockLedgerViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? itemid = null)
        {
            var stockLedgers = from sl in _unitOfWork.StockLedgerRepository.GetAll()
                               join i in _unitOfWork.ItemRepository.GetAll() on sl.Itemid equals i.id into items
                               from i in items.DefaultIfEmpty()
                               join u in _unitOfWork.UnitRepository.GetAll() on sl.Unitid equals u.id into units
                               from u in units.DefaultIfEmpty()
                               where !(sl?.IsInActive ?? true) && !(i?.IsInActive ?? true) && !(u?.IsInActive ?? true)
                               select new StockLedgerViewModel
                               {
                                   id = sl.id,
                                   Itemid = sl.Itemid,
                                   Unitid = sl.Unitid,
                                   Quantity = sl.Quantity,
                                   ExpiredDate = sl.ExpiredDate,
                                   LedgerDate = sl.LedgerDate,
                                   TransactionType = sl.transactionType ?? "Unknown",
                                   ItemName = i?.Name ?? "N/A",
                                   ItemCode = i?.Code ?? "N/A",
                                   UnitName = u?.Name ?? "N/A",
                                   InQty = (sl.transactionType == "Income" || sl.transactionType == "Purchase") ? (sl.Quantity ) : 0,
                                   OutQty = (sl.transactionType == "Damage" || sl.transactionType == "Lost" || sl.transactionType == "Adjustment") || sl.transactionType=="Purchase Delete"? (sl.Quantity ) : 0*-1
                               };

            if (fromDate.HasValue && toDate.HasValue)
            {
                stockLedgers = stockLedgers.Where(sl => sl.LedgerDate >= fromDate.Value && sl.LedgerDate <= toDate.Value);
            }

            if (!string.IsNullOrEmpty(itemid) && itemid != "Select an Item")
            {
                stockLedgers = stockLedgers.Where(sl => sl.Itemid == itemid);
            }

            return stockLedgers.ToList();
        }
    }
}