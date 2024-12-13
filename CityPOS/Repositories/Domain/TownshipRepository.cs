using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class TownshipRepository : BaseRepository<TownshipEntity>,ITownshipRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public TownshipRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<TownshipViewModel> GetTownships()
        {
          return _dbContext.Townships.Where(w=>!w.IsInActive).Select(s=>new TownshipViewModel
          {id=s.id,
          Name=s.Name,

          }).ToList();  
        }

        public IEnumerable<TownshipViewModel> GetTownshipsByCityId(string cityId)
        {
            return _dbContext.Townships
                         .Where(t => t.Cityid == cityId && !t.IsInActive)
                         .Select(t => new TownshipViewModel
                         {
                             id = t.id,
                             Name = t.Name
                         })
                         .ToList();
        }

        public bool IsAlreadyExist(string name) => _dbContext.Townships.Where(w => w.Name == name && !w.IsInActive).Any();
       
    }
}
