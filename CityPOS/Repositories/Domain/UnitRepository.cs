using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class UnitRepository : BaseRepository<UnitEntity>,IUnitRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public UnitRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<UnitViewModel> GetUnits()
        {
           return _dbContext.Units.Where(w=>!w.IsInActive).Select(s=>new UnitViewModel
           {id=s.id,
            Name = s.Name,
           }).ToList();
        }

        public bool IsAlreadyExist(string name)=>_dbContext.Units.Where(w=>w.Name==name && !w.IsInActive).Any();
       
    }
}
