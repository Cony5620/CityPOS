using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface ICustomerService
    { void Create(CustomerViewModel CustomerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(string id);
        void Update(CustomerViewModel CustomerViewModel);
        bool Delete(string id);
        IEnumerable<CustomerViewModel>GetCustomers();
        bool IsAlreadyExist(CustomerViewModel CustomerViewModel);
        string GetNextCustomerCode();
    }
}
