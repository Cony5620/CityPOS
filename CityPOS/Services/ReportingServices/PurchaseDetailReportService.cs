﻿using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using System.Net;

namespace CityPOS.Services.ReportingServices
{
    public class PurchaseDetailReportService : IPurchaseDetailReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseDetailReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IList<PurchaseDetailReportViewModel> PurchaseDetails(string FromDate, string ToDate, string itemid)
        {
            DateTime? fromDateValue = null;
            DateTime? toDateValue = null;

            // Parse the date strings into DateTime objects if they are provided
            if (!string.IsNullOrEmpty(FromDate))
            {
                fromDateValue = DateTime.Parse(FromDate);
            }
            if (!string.IsNullOrEmpty(ToDate))
            {
                toDateValue = DateTime.Parse(ToDate);
            }
                var PurchaseDetails = from pd in _unitOfWork.PurchaseDetailRepository.GetAll()
                                      join p in _unitOfWork.PurchaseRepository.GetAll()
                                      on pd.Purchaseid equals p.id
                                      join i in _unitOfWork.ItemRepository.GetAll()
                                      on pd.Itemid equals i.id
                                      join u in _unitOfWork.UnitRepository.GetAll()
                                      on pd.Unitid equals u.id
                                      join s in _unitOfWork.SupplierRepository.GetAll()
                                      on p.Supplierid equals s.id
                                      join ca in _unitOfWork.CategoryRepository.GetAll()
                                      on i.Categoryid equals ca.id
                                      where
                                      !pd.IsInActive
                                      && !i.IsInActive
                                      && !p.IsInActive
                                      && !u.IsInActive
                                      && !s.IsInActive
                                      && !ca.IsInActive
                                     && (fromDateValue == null || p.PurchaseDate >= fromDateValue)
                                      && (toDateValue == null || p.PurchaseDate <= toDateValue)
                                      && (string.IsNullOrEmpty(itemid) || pd.Itemid == itemid || itemid == "selece Item")
                                      select new PurchaseDetailReportViewModel
                                      {
                                          PurchaseDate = p.PurchaseDate.ToString("yyyy-MM-dd"),
                                          PurchaseVoNo = p.PurchaseVoNO,
                                          SupplierName = s.Name,
                                          CategoryName = ca.Name,
                                          ItemName = i.Name,
                                          UnitName = u.Name,
                                          Quantity = pd.PurchaseQty,
                                          Price = pd.Price,
                                          TotalPrice = pd.TotalPrice,
                                      };
                return PurchaseDetails.ToList();
            
           
            
        }
    }
}
