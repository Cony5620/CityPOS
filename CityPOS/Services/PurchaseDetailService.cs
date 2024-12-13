using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class PurchaseDetailService : IPurchaseDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(PurchaseDetailViewModel purchaseDetailViewModel)
        {
            var purchaseDetail = new PurchaseDetailEntity
            {
                Purchaseid = purchaseDetailViewModel.id,
                Itemid = purchaseDetailViewModel.Itemid,
                Unitid = purchaseDetailViewModel.Unitid,
                PurchaseQty = purchaseDetailViewModel.Quantity,
                Price = purchaseDetailViewModel.Price,
                TotalPrice = purchaseDetailViewModel.TotalPrice,
                ExpiredDate = purchaseDetailViewModel.ExpiredDate, 
            };
            _unitOfWork.PurchaseDetailRepository.Create(purchaseDetail);
            _unitOfWork.Commit();
        }
    }
}
