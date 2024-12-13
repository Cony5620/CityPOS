using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IUnitService
    {
        void Create(UnitViewModel unitViewModel);
        IEnumerable<UnitViewModel> GetAll();
        UnitViewModel GetBy(string id);
        void Update(UnitViewModel unitViewModel);
        bool Delete(string id);
        IEnumerable<UnitViewModel>GetUnits();
        bool IsAlreadyExist(UnitViewModel unitViewModel);
    }
}
