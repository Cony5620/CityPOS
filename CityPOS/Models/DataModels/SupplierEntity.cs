using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Supplier")]
    public class SupplierEntity:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cityid { get; set; }
        public string? Adress { get; set; }
        public string Townshipid { get; set; }
        public string PhoneNo { get; set; }
        public string Company { get; set; }

    }
}
