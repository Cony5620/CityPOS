using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Item")]
    public class ItemEntity:BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Categoryid { get; set; }
        public string Brandid { get; set; }
        public string? photo { get; set; }
        public string Supplierid { get; set; }
        public string BarCode { get; set; }
        public string FDANo { get; set; }

    }
}
