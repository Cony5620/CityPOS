using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("City")]
    public class CityEntity:BaseEntity
    {
        public string Name { get; set; }
    }
}
