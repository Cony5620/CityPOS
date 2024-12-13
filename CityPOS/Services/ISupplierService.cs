using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface ISupplierService
    { void Create(SupplierViewModel SupplierViewModel);
        IEnumerable<SupplierViewModel> GetAll();
        SupplierViewModel GetById(string id);
        void Update(SupplierViewModel SupplierViewModel);
        bool Delete(string id);
        IEnumerable<SupplierViewModel>GetSuppliers();
        bool IsAlreadyExist(SupplierViewModel SupplierViewModel);
        string GetNextSupplierCode();
       
    }
}
