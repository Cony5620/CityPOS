using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class StockBalanceService : IStockBalanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StockBalanceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void CreateOrUpdate(StockBalanceViewModel stockBalanceViewModel)
        {
            try
            {
                string Itemid = stockBalanceViewModel.Itemid;
                string Unitid = stockBalanceViewModel.Unitid;
                DateTime ExpiredDate = stockBalanceViewModel.ExpiredDate;

                var existingStockBalance = _unitOfWork.StockBalanceRepository.GetStockBalanceByItemUnitAndExpiration(Itemid, Unitid, ExpiredDate);


                if (existingStockBalance != null)
                {
                    switch (stockBalanceViewModel.TransactionType)

                    {
                        case "Income":
                            existingStockBalance.Quantity += stockBalanceViewModel.Quantity;
                            break;

                        case "Damage":
                        case "Lost":
                            // Check if new quantity is greater than the old quantity
                            if (stockBalanceViewModel.Quantity > existingStockBalance.Quantity)
                            {
                                throw new Exception("New quantity cannot be greater than the existing quantity.");
                            }
                            existingStockBalance.Quantity -= stockBalanceViewModel.Quantity;
                            if (existingStockBalance.Quantity < 0) existingStockBalance.Quantity = 0;
                            break;

                        case "Adjustment":
                            // Allow adjustment only if new quantity is less than or equal to old quantity
                            if (stockBalanceViewModel.Quantity > existingStockBalance.Quantity)
                            {
                                throw new Exception("New quantity cannot be greater than the existing quantity.");
                            }
                            existingStockBalance.Quantity = stockBalanceViewModel.Quantity; // Direct adjustment
                            break;

                        default:
                            throw new Exception("Invalid transaction type");
                    }

                    _unitOfWork.StockBalanceRepository.Update(existingStockBalance);
                    StockLedgerEntity stockLedgerEntry = new StockLedgerEntity
                    {
                        id = Guid.NewGuid().ToString(),
                        LedgerDate = DateTime.Now,
                        Itemid = Itemid,
                        Unitid = Unitid,
                        Quantity = stockBalanceViewModel.Quantity,
                        transactionType = stockBalanceViewModel.TransactionType,
                        Sourceid = existingStockBalance.id,
                        ExpiredDate = ExpiredDate,
                    };
                    _unitOfWork.StockLedgerRepository.Create(stockLedgerEntry);
                }
                else
                {
                    if (stockBalanceViewModel.TransactionType == "Income")
                    {
                        StockBalanceEntity stockBalanceEntity = new StockBalanceEntity()
                        {
                            id = Guid.NewGuid().ToString(),
                            Categoryid = stockBalanceViewModel.Categoryid,
                            Itemid = Itemid,
                            Unitid = Unitid,
                            Quantity = stockBalanceViewModel.Quantity,
                            ExpiredDate = ExpiredDate,

                        };
                        _unitOfWork.StockBalanceRepository.Create(stockBalanceEntity);

                        StockLedgerEntity stockLedgerEntry = new StockLedgerEntity
                        {
                            id = Guid.NewGuid().ToString(),
                            LedgerDate = DateTime.Now,
                            Itemid = Itemid,
                            Unitid = Unitid,
                            Quantity = stockBalanceViewModel.Quantity,
                            transactionType = stockBalanceViewModel.TransactionType,
                            Sourceid = stockBalanceEntity.id,
                            ExpiredDate = ExpiredDate,
                        };
                        _unitOfWork.StockLedgerRepository.Create(stockLedgerEntry);

                    }
                    else
                    {
                        throw new Exception("Cannot add non-Income transaction on a non-existing stock balance record");
                    }

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error in CreateOrUpdate method: {ex.Message}", ex);
            }
            _unitOfWork.Commit();
        }
        public IEnumerable<StockBalanceViewModel> GetAll(string? categoryid = null,string? itemid=null)
        {
           IEnumerable<StockBalanceViewModel>stockBalanceViews=(from sb in _unitOfWork.StockBalanceRepository.GetAll()
                                                                join i in _unitOfWork.ItemRepository.GetAll()
                                                                on sb.Itemid equals i.id
                                                                join u in _unitOfWork.UnitRepository.GetAll()
                                                                on sb.Unitid equals u.id
                                                                join c in _unitOfWork.CategoryRepository.GetAll()
                                                                on sb.Categoryid equals c.id
                                                               
                                                                where !sb.IsInActive && !i.IsInActive && !u.IsInActive
                                                               &&  !c.IsInActive
                                                               
                                                                select new StockBalanceViewModel
                                                                {
                                                                    id=sb.id,
                                                                    Categoryid=sb.Categoryid,
                                                                     ItemCode=i.Code,
                                                                    Itemid=sb.Itemid,
                                                                    Unitid=sb.Unitid,
                                                                    Quantity=sb.Quantity,
                                                                    ExpiredDate=sb.ExpiredDate,
                                                                    CategoryName=c.Name,
                                                                   
                                                                    ItemName=i.Name,
                                                                    UnitName=u.Name,


                                                                }).AsEnumerable();
            if (!string.IsNullOrEmpty(categoryid))
            {
                stockBalanceViews = stockBalanceViews.Where(sb => sb.Categoryid == categoryid);
            }

         
            if (!string.IsNullOrEmpty(itemid))
            {
                stockBalanceViews = stockBalanceViews.Where(sb => sb.Itemid == itemid);
            }
            return stockBalanceViews;
        }
    }

      
  }

