using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;

namespace CityPOS.Repositories.Domain
{
    public interface ISupplierRepository:IBaseRepository<SupplierEntity>
    {
        IEnumerable<SupplierViewModel> GetSuppliers();
        bool IsAlreadyExist(string Code,string Name);
        string GetNextSupplierCode();
      

    }
}
