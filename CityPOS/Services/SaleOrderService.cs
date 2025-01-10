using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleOrderService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(SaleWithDetailViewModel model)
        {
            var SaleOrderEntity = new SaleOrderEntity
            {
                id = Guid.NewGuid().ToString(),
                SaleDate = model.SaleOrder.SaleDate,
                VoucherNo = model.SaleOrder.VoucherNo,
                Customerid = model.SaleOrder.Customerid,
                SubTotal = model.SaleOrder.SubTotal,
                Tax = model.SaleOrder.Tax,
                TaxPercent = model.SaleOrder.TaxPercent,
                DeliverFees = model.SaleOrder.DeliverFees,
                TotalAmount = model.SaleOrder.TotalAmount,
                DisAmount = model.SaleOrder.DisAmount,
                DisPercent = model.SaleOrder.DisPercent,
                NetTotal = model.SaleOrder.NetTotal,
                CashAmount = model.SaleOrder.CashAmount,
                Balance = model.SaleOrder.Balance,
                Paymentid = "1",
                Userid = "1",
                SaleTime = "1",
                Step = 1,
                TotalReturnAmount = model.SaleOrder.TotalReturnAmount,
                SaleType = model.SaleType,
            };
            _unitOfWork.SaleOrderRepository.Create(SaleOrderEntity);

            foreach (var detail in model.SaleDetails)
            {
                if (model.StockSwitch)
                {
                    var usedBatches = _unitOfWork.StockBalanceRepository.ReduceStockForSale(detail.Itemid, detail.Unitid, detail.Quantity);

                    foreach (var batch in usedBatches)
                    {
                        var stockLedgerEntry = new StockLedgerEntity
                        {
                            id = Guid.NewGuid().ToString(),
                            Itemid = detail.Itemid,
                            Quantity = batch.QuantityUsed,
                            Unitid = detail.Unitid,
                            LedgerDate = DateTime.Now,
                            transactionType = "Sale",
                            Sourceid = SaleOrderEntity.id,
                            ExpiredDate = DateTime.Parse(batch.ExpiredDate), // Use the expiration date from the batch
                            CreatedAt = DateTime.Now
                        };

                        _unitOfWork.StockLedgerRepository.Create(stockLedgerEntry);

                    }

                }
                   var saleDetail = new SaleDetatilEntity
                    {
                        id = Guid.NewGuid().ToString(),
                        Orderid = SaleOrderEntity.id,
                        Itemid = detail.Itemid,
                        Unitid = detail.Unitid,
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        TotalPrice = detail.Total,
                        IsFOC = detail.IsFOC,
                        DisPercent = detail.DisPercent,
                        DisAmount = detail.DisAmount,
                        ActualSaleAmount = detail.Amount,
                    };



                    _unitOfWork.SaleDetailRepository.Create(saleDetail);

                
                _unitOfWork.Commit();
            }
        }

        public IEnumerable<SaleOrderViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? Customerid = null)
        {
            var saleOrder = from so in _unitOfWork.SaleOrderRepository.GetAll()
                            join c in _unitOfWork.CustomerRepository.GetAll()
                            on so.Customerid equals c.id
                            where !so.IsInActive && !c.IsInActive
                            select new SaleOrderViewModel
                            {id=so.id,
                            SaleDate=so.SaleDate,
                            VoucherNo=so.VoucherNo,
                            Customerid=c.id,
                            CustomerName=c.Name,
                            SaleType=so.SaleType,
                            CashAmount=so.CashAmount,

                            };
            return saleOrder.ToList();
                          
        }
    }

 }


