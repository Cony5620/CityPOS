using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(SaleDetailViewModel saleDetailViewModel)
        {
            var SaleDetailEntity = new SaleDetatilEntity
            {
               
                Orderid=saleDetailViewModel.SaleOrderid,
                Itemid = saleDetailViewModel.Itemid,
                Unitid = saleDetailViewModel.Unitid,
                Quantity = saleDetailViewModel.Quantity,
                Price = saleDetailViewModel.Price,

                IsFOC = saleDetailViewModel.IsFOC,
                DisPercent = saleDetailViewModel.DisPercent,
                DisAmount = saleDetailViewModel.DisAmount,
                ActualSaleAmount = saleDetailViewModel.ActualSaleAmount
            };
            _unitOfWork.SaleDetailRepository.Create(SaleDetailEntity);
        }
    }
}
