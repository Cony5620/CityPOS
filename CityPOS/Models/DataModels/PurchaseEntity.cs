using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("Purchase")]
    public class PurchaseEntity:BaseEntity
    {
        public DateTime PurchaseDate { get; set; }
        public string Supplierid { get; set; }
      
        public decimal Disprecent { get; set; }
        public decimal DisAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal CashAmount { get; set; }
        public string PurchaseVoNO { get; set; }
        public virtual ICollection<PurchaseDetailEntity> PurchaseDetails { get; set; } = new List<PurchaseDetailEntity>();
    }
}
