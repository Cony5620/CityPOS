using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Township")]
    public class TownshipEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Cityid { get; set; }
        [ForeignKey("Cityid")]
        public virtual CityEntity City { get; set; }
        public decimal DeliveryFees { get; set; }
    }
}
