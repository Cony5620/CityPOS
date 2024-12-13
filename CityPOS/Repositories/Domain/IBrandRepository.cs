using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IBrandRepository:IBaseRepository<BrandEntity>
    {
        IEnumerable<BrandViewModel> GetBrands();
        bool IsAlreadyExist(string Code,string Name);
        string GetLastBrandCode();
        IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryid);
    }
}
