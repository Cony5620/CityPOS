using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Repositories.Domain
{
    public class CategoryRepository : BaseRepository<CategoryEntity>,ICategoryRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public CategoryRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

  

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            return _dbContext.Categories.Where(w=>!w.IsInActive).Select(s=>new CategoryViewModel
            
            {   id=s.id,
               Code = s.Code + "/" + s.Name,

            }).ToList();
        }

        public string GetLastCategoryCode()
        {
         
            var lastCategory = _dbContext.Categories
                .OrderByDescending(c => c.Code)
                .FirstOrDefault();
            return lastCategory != null ? lastCategory.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name) => _dbContext.Categories.Where(w => (w.Code != Code && w.Name == Name) && !w.IsInActive).Any();
       
    }
}
