using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class PriceService : IPriceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PriceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(PriceViewModel PriceViewModel)
        {

            PriceEntity PriceEntity = new PriceEntity()
            {
                id = Guid.NewGuid().ToString(),
                Itemid=PriceViewModel.Itemid,
                Unitid= PriceViewModel.Unitid,
                PricingDate=PriceViewModel.PricingDate,
                PurchasePrice=PriceViewModel.PurchasePrice,
                WholeSalePrice=PriceViewModel.WholeSalePrice,
                RetailSalePrice=PriceViewModel.RetailSalePrice,
                
                 CreatedAt = DateTime.Now,

            };
           _unitOfWork.PriceRepository.Create(PriceEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {

            try
            {
                PriceEntity PriceEntity = _unitOfWork.PriceRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (PriceEntity != null)
                {
                    PriceEntity.IsInActive = true;
                    _unitOfWork.PriceRepository.Update(PriceEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<PriceViewModel> GetAll()
        {
            IEnumerable<PriceViewModel> Prices = (from p in _unitOfWork.PriceRepository.GetAll()
                                                  join i in _unitOfWork.ItemRepository.GetAll()
                                                  on p.Itemid equals i.id
                                                  join u in _unitOfWork.UnitRepository.GetAll()
                                                   on p.Unitid equals u.id
                                                 
                                                  where !p.IsInActive && !i.IsInActive && !u.IsInActive
                                                  select new PriceViewModel
                                                  {
                                                      id = p.id,
                                                      Itemid = p.Itemid,
                                                      ItemName=i.Name,
                                                      Unitid = p.Unitid,
                                                      UnitName= u.Name,
                                                      PricingDate = p.PricingDate,
                                                      PurchasePrice = p.PurchasePrice,
                                                      WholeSalePrice = p.WholeSalePrice,
                                                      RetailSalePrice = p.RetailSalePrice,
                                                      ModifiedAt = p.ModifiedAt,

                                                  }).ToList();
            return Prices;
        }

        public IEnumerable<PriceViewModel> GetPrices()
        {
            return _unitOfWork.PriceRepository.GetPrices();
            
        }

        public PriceViewModel GetById(string id)
        {
           return _unitOfWork.PriceRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s => new PriceViewModel
            {

               id = s.id,
               Itemid = s.Itemid,
               Unitid = s.Unitid,
               PricingDate = s.PricingDate,
               PurchasePrice = s.PurchasePrice,
               WholeSalePrice = s.WholeSalePrice,
               RetailSalePrice = s.RetailSalePrice,

               CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        }

        public void Update(PriceViewModel PriceViewModel)
        {
            PriceEntity PriceEntity = new PriceEntity()
            {
                id =PriceViewModel.id,
                Itemid = PriceViewModel.Itemid,
                Unitid = PriceViewModel.Unitid,
                PricingDate = PriceViewModel.PricingDate,
                PurchasePrice = PriceViewModel.PurchasePrice,
                WholeSalePrice = PriceViewModel.WholeSalePrice,
                RetailSalePrice = PriceViewModel.RetailSalePrice,
                CreatedAt = PriceViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,

            };
            _unitOfWork.PriceRepository.Update(PriceEntity);
            _unitOfWork.Commit();
        }

        public ItemPriceViewModel GetItemDetailsByBarcode(string barcode)
        {
           return _unitOfWork.PriceRepository.GetItemDetailsByBarcode(barcode);
        }

    }
}
