using CityPOS.Models.ViewModels;

namespace CityPOS.Services
{
    public interface IAspNetRolesService
    {
        Task CreateAsync(AspNetRolesViewModel aspNetRolesViewModel);
        Task<IEnumerable<AspNetRolesViewModel>> GetAllAsync();
        Task DeleteAsync(string id);
    }
}
