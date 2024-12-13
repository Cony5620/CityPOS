using CityPOS.Models.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityPOS.Models.ViewModels
{
    public class BrandViewModel
    {
        public string id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Categoryid { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
