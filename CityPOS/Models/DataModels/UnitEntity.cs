using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Unit")]
    public class UnitEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
