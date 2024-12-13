using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface ITownshipService
    {
        void Create(TownshipViewModel townshipViewModel);
        IEnumerable<TownshipViewModel> GetAll();
        TownshipViewModel GetById(string id);
        void Update(TownshipViewModel townshipViewModel);
        bool Delete(string id);
        IEnumerable<TownshipViewModel>GetTownships();
        bool IsAlreadyExist(TownshipViewModel townshipViewModel);
        IEnumerable<TownshipViewModel> GetTownshipsByCityId(string cityId);
    }
}
