using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class BrandRepository : BaseRepository<BrandEntity>,IBrandRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public BrandRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<BrandViewModel> GetBrands()
        {
            return _dbContext.Brands.Where(w=>!w.IsInActive).Select(s=>new BrandViewModel
            {
                id=s.id,
                Name=s.Name,

            }).ToList();
        }
        public IEnumerable<BrandViewModel> GetBrandsByCategory(string Categoryid)
        {
            return _dbContext.Brands
                           .Where(b => b.Categoryid == Categoryid && !b.IsInActive)
                           .Select(b => new BrandViewModel
                           {
                               id = b.id,
                               Name = b.Name
                           })
                           .ToList();
        }
        public string GetLastBrandCode()
        {
            var lastBrand = _dbContext.Brands
              .OrderByDescending(b => b.Code)
              .FirstOrDefault();
            return lastBrand != null ? lastBrand.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name)=> _dbContext.Brands.Where(w => (w.Code != Code && w.Name == Name) && !w.IsInActive).Any();


    }
}
