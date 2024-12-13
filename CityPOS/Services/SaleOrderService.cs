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
            throw new NotImplementedException();
        }
    }
}
