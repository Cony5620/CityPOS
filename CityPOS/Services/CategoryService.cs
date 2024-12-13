using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(CategoryViewModel categoryViewModel)
        {
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                id = Guid.NewGuid().ToString(),
                Code = categoryViewModel.Code,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
                CreatedAt = DateTime.Now,

            };
           _unitOfWork.CategoryRepository.Create(categoryEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {

            try
            {
                CategoryEntity categoryEntity = _unitOfWork.CategoryRepository.GetBy(w => w.id == id).SingleOrDefault();
                if (categoryEntity != null)
                {
                    categoryEntity.IsInActive = true;
                    _unitOfWork.CategoryRepository.Update(categoryEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            return _unitOfWork.CategoryRepository.GetAll().Where(w => !w.IsInActive).Select(s => new CategoryViewModel
            {
                id = s.id,
                Code = s.Code,
                Name = s.Name,
                Description = s.Description,

            }).AsEnumerable();
           
        }

     

        public CategoryViewModel GetById(string id)
        {
           return _unitOfWork.CategoryRepository.GetBy(w => w.id == id && !w.IsInActive).Select(s => new CategoryViewModel
            {
                id = s.id,
                Code = s.Code,
                Name = s.Name,
                Description = s.Description,
                CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _unitOfWork.CategoryRepository.GetCategories();
        }

        public string GetNextCategoryCode()
        {
            var lastCode = _unitOfWork.CategoryRepository.GetLastCategoryCode();

            if (lastCode != null)
            {
                // Increment the last code by 1, assuming codes are numeric (e.g., 001, 002)
                int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3"); // Ensure the new code is formatted with leading zeros
            }
            else
            {
                // If no categories exist, start with 001
                return "001";
            }

        }

        public bool IsAlreadyExist(CategoryViewModel categoryViewModel)
        {
          return  _unitOfWork.CategoryRepository.IsAlreadyExist(categoryViewModel.Code, categoryViewModel.Name);
        }

        public void Update(CategoryViewModel categoryViewModel)
        {
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                id = categoryViewModel.id,
                Code = categoryViewModel.Code,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description,
                CreatedAt = categoryViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,

            };
            _unitOfWork.CategoryRepository.Update(categoryEntity);
            _unitOfWork.Commit();
        }
    }
}
