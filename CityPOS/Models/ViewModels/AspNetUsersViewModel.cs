using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CityPOS.Models.ViewModels
{
    public class AspNetUsersViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        public string SelectedRole { get; set; }  // This is what needs to be validated

       
    }
}
