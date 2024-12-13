using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface ICustomerRepository:IBaseRepository<CustomerEntity>
    {
        IEnumerable<CustomerViewModel> GetCustomers();
        bool IsAlreadyExist(string Code,string Name);
        string GetNextCustomerCode();
    }
}
