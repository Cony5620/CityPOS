using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("AspNetRoles")]
    public class AspNetRolesEntity:IdentityRole
    {
       
    }
}
