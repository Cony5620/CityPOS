using CityPOS.Models.ViewModels;

namespace CityPOS.Services.ReportingServices
{
    public interface ISaleDetailReportService
    {IList<SaleDetailReoprtViewModel>SaleDetails(string fromDate, string toDate,string itemid);
    }
}
