using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CityPOS.Services
{
    public class AspNetRolesService : IAspNetRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AspNetRolesService(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            this._unitOfWork = unitOfWork;
            this._roleManager = roleManager;
        }

      
        public async Task CreateAsync(AspNetRolesViewModel aspNetRolesViewModel)
        {
            var roleExist = await _roleManager.RoleExistsAsync(aspNetRolesViewModel.Name);
            if (!roleExist)
            {
                var role = new IdentityRole
                {
                    Name = aspNetRolesViewModel.Name,
                    NormalizedName = aspNetRolesViewModel.Name.ToUpper()
                };

                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception($"Error creating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

           
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id); // Use FindByIdAsync to retrieve the role by its ID
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role); // Use RoleManager to delete the role
            if (!result.Succeeded)
            {
                throw new Exception($"Error deleting role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }


        public async Task<IEnumerable<AspNetRolesViewModel>> GetAllAsync()
        {
            var roles = _roleManager.Roles.ToList();
            var roleViewModels = roles.Select(role => new AspNetRolesViewModel
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

          
            return await Task.FromResult(roleViewModels);
        }
    }
}
