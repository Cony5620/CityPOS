using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.DataModels
{
    [Table("SaleOrder")]
    public class SaleOrderEntity:BaseEntity
    {
       
        public DateTime SaleDate { get; set; }
        public string VoucherNo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetTotal { get; set; }
        public decimal CashAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Tax { get; set; }
        public decimal Balance { get; set; }
        public string Paymentid { get; set; }
        public string Customerid { get; set; }
        public int Step { get; set; }
        public decimal TotalReturnAmount { get; set; }
        public decimal DeliverFees { get; set; }
        public string Userid { get; set; }
        public decimal DisPercent { get; set; }
        public decimal DisAmount { get; set; }
        public string SaleTime { get; set; }
    }
}
