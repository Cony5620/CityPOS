using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(BrandViewModel brandViewModel)
        {

            BrandEntity brandEntity = new BrandEntity()
            {
                id = Guid.NewGuid().ToString(),
                Code = brandViewModel.Code,
                Name = brandViewModel.Name,
                Categoryid = brandViewModel.Categoryid,
                CreatedAt = DateTime.Now,

            };
           _unitOfWork.BrandRepository.Create(brandEntity);
            _unitOfWork.Commit();
        }
        public IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryId)
        {
            return _unitOfWork.BrandRepository.GetBrandsByCategory(categoryId);
        }
        public bool Delete(string id)
        {

            try
            {
                BrandEntity brandEntity = _unitOfWork.BrandRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (brandEntity != null)
                {
                    brandEntity.IsInActive = true;
                    _unitOfWork.BrandRepository.Update(brandEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<BrandViewModel> GetAll()
        {
           IEnumerable<BrandViewModel>brands= (from b in _unitOfWork.BrandRepository.GetAll()
                                            join c in _unitOfWork.CategoryRepository.GetAll()
                                            on b.Categoryid equals c.id
                                            where !b.IsInActive && !c.IsInActive
                                            select new BrandViewModel
                                            {
                                                id = b.id,
                                                Code = b.Code,
                                                Name = b.Name,
                                                Categoryid = b.Categoryid,
                                                CategoryName = c.Name,

                                            }).ToList();
            return brands;
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            return _unitOfWork.BrandRepository.GetBrands();
            
        }

        public BrandViewModel GetById(string id)
        {
           return _unitOfWork.BrandRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s => new BrandViewModel
            {
                id = s.id,
                Code = s.Code,
                Name = s.Name,
                Categoryid = s.Categoryid,
                CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        } 

        public string GetLastBrandCode()
        {
            var lastCode = _unitOfWork.BrandRepository.GetLastBrandCode();

            if (lastCode != null)
            {
              
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3");
            }
            else
            {
         
                return "001";
            }
        }

        public bool IsAlreadyExist(BrandViewModel brandViewModel)
        {
            return _unitOfWork.BrandRepository.IsAlreadyExist(brandViewModel.Code,brandViewModel.Name);
        }

        public void Update(BrandViewModel brandViewModel)
        {
            BrandEntity brandEntity = new BrandEntity()
            {
                id = brandViewModel.id,
                Code = brandViewModel.Code,
                Name = brandViewModel.Name,
                Categoryid = brandViewModel.Categoryid,
                CreatedAt = brandViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,

            };
            _unitOfWork.BrandRepository.Update(brandEntity);
            _unitOfWork.Commit();
        }
    }
}
