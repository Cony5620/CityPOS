using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using System.Linq;
using System.Net;

namespace CityPOS.Services.ReportingServices
{
    public class SaleDetailReportService : ISaleDetailReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleDetailReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IList<SaleDetailReoprtViewModel> SaleDetails(string fromDate, string toDate, string itemid)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;

            if (!string.IsNullOrEmpty(fromDate)) fromDateValue = DateTime.Parse(fromDate);
            if (!string.IsNullOrEmpty(toDate)) toDateValue = DateTime.Parse(toDate);


            if (itemid is not null && itemid != "Select Item") 
            {
                var SaleDetails = from sd in _unitOfWork.SaleDetailRepository.GetAll()
                                  join so in _unitOfWork.SaleOrderRepository.GetAll()
                                  on sd.Orderid equals so.id
                                  join i in _unitOfWork.ItemRepository.GetAll()
                                  on sd.Itemid equals i.id
                                  join u in _unitOfWork.UnitRepository.GetAll()
                                  on sd.Unitid equals u.id
                                  join c in _unitOfWork.CustomerRepository.GetAll()
                                  on so.Customerid equals c.id
                                  join ca in _unitOfWork.CategoryRepository.GetAll()
                                  on i.Categoryid equals ca.id

                                  where
                                  !sd.IsInActive
                                  && !i.IsInActive
                                  && !so.IsInActive
                                  && !u.IsInActive
                                  && !c.IsInActive
                                  && !ca.IsInActive
                                  && (fromDateValue == null || so.SaleDate >= fromDateValue)
                                  && (toDateValue == null || so.SaleDate <= toDateValue)
                                  && sd.Itemid == itemid
                                  select new SaleDetailReoprtViewModel
                                  {
                                      SaleDate = so.SaleDate.ToString("yyyy-MM-dd"),
                                      VoucherNo = so.VoucherNo,
                                      CustomerName = c.Name,
                                      SaleType = so.SaleType,
                                      CategoryName = ca.Name,
                                      ItemName = i.Name,
                                      UnitName = u.Name,
                                      Quantity = sd.Quantity,
                                      Price = sd.Price,
                                      Total = sd.TotalPrice,
                                      Amount = sd.ActualSaleAmount,
                                      IsFOC = sd.IsFOC ? "Yes" : "No",
                                      DisAmount = sd.DisAmount,
                                      DisPercent = sd.DisPercent,

                                  };
                return SaleDetails.ToList();
            }
            else
            {
                var SaleDetails = from sd in _unitOfWork.SaleDetailRepository.GetAll()
                                  join so in _unitOfWork.SaleOrderRepository.GetAll()
                                  on sd.Orderid equals so.id
                                  join i in _unitOfWork.ItemRepository.GetAll()
                                  on sd.Itemid equals i.id
                                  join u in _unitOfWork.UnitRepository.GetAll()
                                  on sd.Unitid equals u.id
                                  join c in _unitOfWork.CustomerRepository.GetAll()
                                  on so.Customerid equals c.id
                                  join ca in _unitOfWork.CategoryRepository.GetAll()
                                  on i.Categoryid equals ca.id

                                  where
                                  !sd.IsInActive
                                  && !i.IsInActive
                                  && !so.IsInActive
                                  && !u.IsInActive
                                  && !c.IsInActive
                                  && !ca.IsInActive
                                  && (fromDateValue == null || so.SaleDate >= fromDateValue)
                                  && (toDateValue == null || so.SaleDate <= toDateValue)
                                
                                  select new SaleDetailReoprtViewModel
                                  {
                                      SaleDate = so.SaleDate.ToString("yyyy-MM-dd"),
                                      VoucherNo = so.VoucherNo,
                                      CustomerName = c.Name,
                                      SaleType = so.SaleType,
                                      CategoryName = ca.Name,
                                      ItemName = i.Name,
                                      UnitName = u.Name,
                                      Quantity = sd.Quantity,
                                      Price = sd.Price,
                                      Total = sd.TotalPrice,
                                      Amount = sd.ActualSaleAmount,
                                      IsFOC = sd.IsFOC ? "Yes" : "No",
                                      DisAmount = sd.DisAmount,
                                      DisPercent = sd.DisPercent,

                                  };
                return SaleDetails.ToList();
            }
                
        }
    }
}
