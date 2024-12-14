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
            };
            _unitOfWork.SaleOrderRepository.Create(SaleOrderEntity);

            foreach (var detail in model.SaleDetails)
            {
                var saleDetail = new SaleDetatilEntity
                {
                    id = Guid.NewGuid().ToString(),
                    Orderid = SaleOrderEntity.id,
                    Itemid = detail.Itemid,
                    Unitid=detail.Unitid,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                  
                    IsFOC = detail.IsFOC,
                    DisPercent = detail.DisPercent,
                    DisAmount = detail.DisAmount,
                    ActualSaleAmount = detail.ActualSaleAmount
                };

                _unitOfWork.SaleDetailRepository.Create(saleDetail);

            }
            _unitOfWork.Commit();
            }
    }
}
