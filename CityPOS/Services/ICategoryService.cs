using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using System.Linq.Expressions;

namespace CityPOS.Services
{
    public interface ICategoryService
    {
        void Create(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> GetAll();
        CategoryViewModel GetById(string id);
        void Update(CategoryViewModel categoryViewModel);
        bool Delete(string id);
        IEnumerable<CategoryViewModel>GetCategories();
        bool IsAlreadyExist(CategoryViewModel categoryViewModel);
        string GetNextCategoryCode();
      
    }
}
