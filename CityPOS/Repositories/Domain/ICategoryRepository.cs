using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface ICategoryRepository:IBaseRepository<CategoryEntity>
    {
        IEnumerable<CategoryViewModel> GetCategories();
        bool IsAlreadyExist(string Code,string Name);
        string GetLastCategoryCode();
     
    }
}
