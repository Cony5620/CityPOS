using CityPOS.Models.ViewModels;

namespace CityPOS.Services.ReportingServices
{
    public interface IPurchaseDetailReportService
    {IList<PurchaseDetailReportViewModel>PurchaseDetails(string FromDate, string ToDate,string itemid);
    }
}
