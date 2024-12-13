using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(ItemViewModel ItemViewModel, PriceViewModel priceViewModel,string photoUrl )
        {

            ItemEntity ItemEntity = new ItemEntity()
            {
                id = Guid.NewGuid().ToString(),
                Code = ItemViewModel.Code,
                Name = ItemViewModel.Name,
                photo=photoUrl,
                Categoryid=ItemViewModel.Categoryid,
                Brandid=ItemViewModel.Brandid,
                Supplierid=ItemViewModel.Supplierid,
                FDANo=ItemViewModel.FDANo,
                BarCode=ItemViewModel.BarCode,

                CreatedAt = DateTime.Now,

            };
           _unitOfWork.ItemRepository.Create(ItemEntity);

            PriceEntity priceEntity = new PriceEntity
            {
                id = Guid.NewGuid().ToString(),
                Itemid = ItemEntity.id, // Link to the saved ItemEntity
          
                Unitid = priceViewModel.Unitid,
                PricingDate =DateTime.Now,
                PurchasePrice = priceViewModel.PurchasePrice,
                WholeSalePrice = priceViewModel.WholeSalePrice,
                RetailSalePrice = priceViewModel.RetailSalePrice,
                CreatedAt = DateTime.Now,
            };

            // Save the PriceEntity to the database
            _unitOfWork.PriceRepository.Create(priceEntity);
            _unitOfWork.Commit();

        }

        public bool Delete(string id)
        {

            try
            {
                ItemEntity ItemEntity = _unitOfWork.ItemRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (ItemEntity != null)
                {
                    ItemEntity.IsInActive = true;
                    _unitOfWork.ItemRepository.Update(ItemEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<ItemViewModel> GetAll()
        {
           IEnumerable<ItemViewModel>Items= (from i in _unitOfWork.ItemRepository.GetAll()
                                            join c in _unitOfWork.CategoryRepository.GetAll()
                                            on i.Categoryid equals c.id
                                            join b in _unitOfWork.BrandRepository.GetAll()
                                             on i.Brandid equals b.id
                                             join s in _unitOfWork.SupplierRepository.GetAll()
                                             on i.Supplierid equals s.id
                                             where !c.IsInActive && !i.IsInActive && !b.IsInActive
                                            select new ItemViewModel
                                            {
                                                id = i.id,
                                                Code = i.Code,
                                                Name = i.Name,
                                                photo = i.photo,
                                                Categoryid = i.Categoryid,
                                                CategoryName=c.Name,
                                                Brandid = i.Brandid,
                                                BrandName=b.Name,
                                                Supplierid = i.Supplierid,
                                                SupplierName=s.Name,
                                                FDANo = i.FDANo,
                                                BarCode = i.BarCode,

                                            }).ToList();
            return Items;
        }

        public IEnumerable<ItemViewModel> GetItems()
        {
            return _unitOfWork.ItemRepository.GetItems();
            
        }

        public ItemViewModel GetById(string id)
        {
           return _unitOfWork.ItemRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s => new ItemViewModel
            {

               id = s.id,
               Code = s.Code,
               Name = s.Name,
               photo = s.photo,
               Categoryid = s.Categoryid,
               Brandid = s.Brandid,
               Supplierid = s.Supplierid,
               FDANo = s.FDANo,
               BarCode = s.BarCode,
               CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        }

        public void Update(ItemViewModel ItemViewModel, string photoUrl)
        {

            var Existingitem = _unitOfWork.ItemRepository.GetBy(w => w.id == ItemViewModel.id).FirstOrDefault();
            if (Existingitem == null)
            {
                throw new Exception(" Items not found.");
            }
            Existingitem.id = ItemViewModel.id;
            Existingitem.Code = ItemViewModel.Code;
            Existingitem.Name = ItemViewModel.Name;
            Existingitem.photo = string.IsNullOrEmpty(photoUrl) ? Existingitem.photo : photoUrl;
            Existingitem.Categoryid = ItemViewModel.Categoryid;
            Existingitem.Brandid = ItemViewModel.Brandid;
            Existingitem.Supplierid = ItemViewModel.Supplierid;
            Existingitem.FDANo = ItemViewModel.FDANo;
            Existingitem.BarCode = ItemViewModel.BarCode;
            Existingitem.CreatedAt = ItemViewModel.CreatedAt;
            Existingitem.ModifiedAt = DateTime.Now;
            _unitOfWork.ItemRepository.Update(Existingitem);
            _unitOfWork.Commit();
        }

        public bool IsAlreadyExist(ItemViewModel ItemViewModel)
        {
            return _unitOfWork.ItemRepository.IsAlreadyExist(ItemViewModel.Code, ItemViewModel.Name);
        }

        public string GetNextItemCode()
        {
            var lastCode = _unitOfWork.ItemRepository.GetNextItemCode();
            if (lastCode != null)
            {
                int newCode=int.Parse(lastCode)+1; 
                return newCode.ToString("D3");
            }
            else
            {
                return "001";
            }
        }

        public IEnumerable<ItemViewModel> GetItemsByCategory(string categoryId)
        {
          return _unitOfWork.ItemRepository.GetItemsByCategory(categoryId);
        }

       
    }
}
