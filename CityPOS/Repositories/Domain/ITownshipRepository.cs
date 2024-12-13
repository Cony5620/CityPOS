using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface ITownshipRepository:IBaseRepository<TownshipEntity>
    { 
       IEnumerable<TownshipViewModel>GetTownships();
        bool IsAlreadyExist(string name);
        IEnumerable<TownshipViewModel> GetTownshipsByCityId(string cityId);
    }
}
