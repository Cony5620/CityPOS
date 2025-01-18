using CityPOS.Models.ViewModels;

namespace CityPOS.Services.ReportingServices
{
    public interface IProfitReportService
    {
        IList<ProfitReportViewModel> GetProfitReport(string fromDate, string toDate);
    }
}
