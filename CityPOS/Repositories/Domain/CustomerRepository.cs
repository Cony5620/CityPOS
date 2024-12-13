using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class CustomerRepository : BaseRepository<CustomerEntity>,ICustomerRepository
    {
        private readonly CityPOSDbContext _dbContext;

        public CustomerRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            return _dbContext.Customers.Where(w => !w.IsInActive).Select(s => new CustomerViewModel
            {
                id = s.id,
                Name = s.Name,

            }).ToList();
        }

        public string GetNextCustomerCode()
        {
            var lastCustomer = _dbContext.Customers
                  .OrderByDescending(c => c.Code)
                  .FirstOrDefault();
            return lastCustomer != null ? lastCustomer.Code : null;
        }

        public bool IsAlreadyExist(string Code, string Name) => _dbContext.Customers.Where(w => (w.Code != Code && w.Name == Name) && !w.IsInActive).Any();
       
    }
}
