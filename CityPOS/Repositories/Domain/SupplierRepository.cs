using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class SupplierRepository : BaseRepository<SupplierEntity>,ISupplierRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public SupplierRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public string GetNextSupplierCode()
        {
            var lastSupplier = _dbContext.Suppliers
                  .OrderByDescending(s => s.Code)
                  .FirstOrDefault();
            return lastSupplier != null ? lastSupplier.Code : null;
        } 

        public IEnumerable<SupplierViewModel> GetSuppliers()
        {
            return _dbContext.Suppliers.Where(w => !w.IsInActive).Select(s => new SupplierViewModel
            {
                id = s.id,
                Name = s.Name,

            }).ToList();
        }

     

        public bool IsAlreadyExist(string Code, string Name)=>_dbContext.Suppliers.Where(w=>(w.Code!=Code && w.Name==Name)&& !w.IsInActive).Any();
       
    }
}
