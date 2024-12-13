using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface ICityService
    {   void Create(CityViewModel cityViewModel);
        IEnumerable<CityViewModel> GetAll();
        CityViewModel GetById(string id);
        void Update(CityViewModel cityViewModel);
        bool Delete(string id);
        IEnumerable<CityViewModel>GetCities();
        bool IsAlreadyExist(CityViewModel cityViewModel);


    }
}
