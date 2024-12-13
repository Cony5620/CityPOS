using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IBrandService
    { void Create(BrandViewModel brandViewModel);
        IEnumerable<BrandViewModel> GetAll();
        BrandViewModel GetById(string id);
        void Update(BrandViewModel brandViewModel);
        bool Delete(string id);
        IEnumerable<BrandViewModel>GetBrands();
        bool IsAlreadyExist(BrandViewModel brandViewModel);
        string GetLastBrandCode();
        IEnumerable<BrandViewModel> GetBrandsByCategory(string categoryId);
    }
}
