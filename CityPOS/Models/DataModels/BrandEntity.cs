using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Brand")]
    public class BrandEntity:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Categoryid { get; set; }
        [ForeignKey("Categoryid")]
        public virtual CategoryEntity Category { get; set; }
     
    }
}
