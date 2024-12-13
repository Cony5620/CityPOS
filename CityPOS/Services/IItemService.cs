using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IItemService
    { void Create(ItemViewModel ItemViewModel,PriceViewModel priceViewModel,string photoUrl);
        IEnumerable<ItemViewModel> GetAll();
        ItemViewModel GetById(string id);
        void Update(ItemViewModel ItemViewModel, string photoUrl);
        bool Delete(string id);
        IEnumerable<ItemViewModel>GetItems();
        bool IsAlreadyExist (ItemViewModel ItemViewModel);
        string GetNextItemCode();
        IEnumerable<ItemViewModel> GetItemsByCategory(string categoryId);
       
    }
}
