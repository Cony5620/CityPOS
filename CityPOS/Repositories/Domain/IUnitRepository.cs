using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IUnitRepository:IBaseRepository<UnitEntity>
    {
        IEnumerable<UnitViewModel> GetUnits();
        bool IsAlreadyExist(string name);
    }
}
