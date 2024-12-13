using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface ICityRepository:IBaseRepository<CityEntity>
    {
        IEnumerable<CityViewModel> GetCities();
        bool IsAlreadyExist(string Name);
    }
}
