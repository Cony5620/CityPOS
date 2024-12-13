using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class CityRepository : BaseRepository<CityEntity>,ICityRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public CityRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<CityViewModel> GetCities()
        {
          return _dbContext.Citys.Where(w=>!w.IsInActive).Select(s=>new CityViewModel
          {id=s.id,
          Name=s.Name,

          }).ToList();
        }

        public bool IsAlreadyExist(string Name)=> _dbContext.Citys.Where(w => w.Name == Name && !w.IsInActive).Any();

    }
}
