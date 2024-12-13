using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class ItemRepository : BaseRepository<ItemEntity>,IItemRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public ItemRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public ItemEntity FindById(string itemId)
        {
            return _dbContext.Items.FirstOrDefault(item => item.id == itemId);
        }

        public IEnumerable<ItemViewModel> GetItems()
        {
            return _dbContext.Items.Where(w => !w.IsInActive).Select(s => new ItemViewModel
            {
                id = s.id,
                Name = s.Name,

            }).ToList();
        }

     

        public IEnumerable<ItemViewModel> GetItemsByCategory(string categoryid)
        {
            return _dbContext.Items
                            .Where(i=> i.Categoryid == categoryid && !i.IsInActive)
                            .Select(i => new ItemViewModel
                            {
                                id = i.id,
                                Name = i.Name
                            })
                            .ToList();
        }

        public string GetNextItemCode()
        {
            var lastitem = _dbContext.Items
              .OrderByDescending(i => i.Code)
              .FirstOrDefault();
            return lastitem != null ? lastitem.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name) => _dbContext.Items.Where(w => (w.Code != Code && w.Name == Name) && !w.IsInActive).Any();
       
    }
}
