using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface IItemRepository:IBaseRepository<ItemEntity>
    {
        IEnumerable<ItemViewModel> GetItems();
        bool IsAlreadyExist(string Code,string Name);
        string GetNextItemCode();
        IEnumerable<ItemViewModel> GetItemsByCategory(string categoryid);
        ItemEntity FindById(string itemId);
      
    }
}
