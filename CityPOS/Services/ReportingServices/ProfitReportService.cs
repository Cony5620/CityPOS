using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services.ReportingServices
{
    public class ProfitReportService : IProfitReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfitReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IList<ProfitReportViewModel> GetProfitReport(string fromDate, string toDate)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;
           
            if (!string.IsNullOrEmpty(fromDate)) fromDateValue = DateTime.Parse(fromDate);
            if (!string.IsNullOrEmpty(toDate)) toDateValue = DateTime.Parse(toDate);

            var profitData = from sd in _unitOfWork.SaleDetailRepository.GetAll()
                             join s in _unitOfWork.SaleOrderRepository.GetAll()
                             on sd.Orderid equals s.id
                             join i in _unitOfWork.ItemRepository.GetAll()
                             on sd.Itemid equals i.id
                             join c in _unitOfWork.CategoryRepository.GetAll() 
                             on i.Categoryid equals c.id
                             join pd in _unitOfWork.PurchaseDetailRepository.GetAll()
                             on i.id equals pd.Itemid
                             where !s.IsInActive && !sd.IsInActive &&
                                   (fromDateValue == null || s.SaleDate >= fromDateValue) &&
                                   (toDateValue == null || s.SaleDate <= toDateValue)
                             select new ProfitReportViewModel
                             {
                                 SaleDate = s.SaleDate.ToString("yyyy-MM-dd"),
                                 InvoiceNumber = s.VoucherNo,
                                 ItemName = i.Name,
                                 CategoryName = c.Name,
                                 QuantitySold = sd.Quantity,
                                 OriginalSellingPrice = sd.Price,
                                 PurchasePrice = pd.Price,
                                 DiscountPercentage = sd.DisPercent,
                                 DiscountAmount = (sd.Price * sd.DisPercent / 100) ,
                                 NetSalePrice = sd.Price - (sd.Price * sd.DisPercent / 100),
                                 TotalRevenue = (sd.Price - (sd.Price * sd.DisPercent / 100)) * sd.Quantity,
                                 TotalCost = pd.Price * sd.Quantity,
                                 Profit = ((sd.Price - (sd.Price * sd.DisPercent / 100)) * sd.Quantity) - (pd.Price * sd.Quantity),
                                 ProfitMargin = (((sd.Price - (sd.Price * sd.DisPercent / 100)) * sd.Quantity) - (pd.Price * sd.Quantity)) /
                                                ((sd.Price - (sd.Price * sd.DisPercent / 100)) * sd.Quantity) * 100
                             };
           
           
            return profitData.ToList();
        }
    }
}
