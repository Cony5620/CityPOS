using CityPOS.Utility;
using System.ComponentModel.DataAnnotations;

namespace CityPOS.Models.DataModels
{
    public abstract class BaseEntity
    {
        [Key]
        public string id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Ip { get; set; }=NetworkHelper.GetLocalIPAddress();
        public string? Remark { get; set; }
        public bool IsInActive { get; set; }
       
    }
}
