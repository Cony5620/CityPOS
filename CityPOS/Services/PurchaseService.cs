using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class PurchaseService :IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Create(PurchaseWithDetailViewModel models)
        {
           
            var purchaseViewModel = models.Purchase;
            var purchaseDetailViewModels = models.ItemDetails;
            decimal purchaseTotal = 0;
            foreach(var i in purchaseDetailViewModels)
            {
                purchaseTotal += i.TotalPrice;
            }
         
            var purchaseEntity = new PurchaseEntity()
            {
                id = Guid.NewGuid().ToString(),
                PurchaseDate = purchaseViewModel.PurchaseDate,
                PurchaseVoNO = purchaseViewModel.PurchaseVoNo,
                Supplierid = purchaseViewModel.Supplierid,
                TotalAmount = purchaseTotal,
                CashAmount = purchaseTotal,
                CreatedAt = DateTime.UtcNow,
            };

            
            _unitOfWork.PurchaseRepository.Create(purchaseEntity);

           
            foreach (var detail in purchaseDetailViewModels)
            {
                var purchaseDetailEntity = new PurchaseDetailEntity
                {
                    id = Guid.NewGuid().ToString(),
                    Purchaseid = purchaseEntity.id, 
                    Itemid = detail.Itemid,
                    Unitid = detail.Unitid,
                    PurchaseQty = detail.Quantity,
                    Price = detail.Price,
                    TotalPrice = detail.TotalPrice,
                    ExpiredDate = detail.ExpiredDate,
                    CreatedAt = DateTime.UtcNow,
                };
                 var categoryid = _unitOfWork.ItemRepository.FindById(detail.Itemid)?.Categoryid;

                _unitOfWork.StockBalanceRepository.UpdateStockBalanceByItemUnitAndExpiration(detail.Itemid,detail.Unitid,detail.ExpiredDate,detail.Quantity, categoryid);

                var stockLedgerEntry = new StockLedgerEntity
                {
                    id = Guid.NewGuid().ToString(),
                    Itemid = detail.Itemid,
                    Quantity = detail.Quantity,
                    Unitid = detail.Unitid,
                    LedgerDate = DateTime.Now,
                    transactionType = "Purchase",  
                    Sourceid = purchaseEntity.id,
                    ExpiredDate = detail.ExpiredDate,
                    CreatedAt = DateTime.Now
                };
                _unitOfWork.StockLedgerRepository.Create(stockLedgerEntry);

                _unitOfWork.PurchaseDetailRepository.Create(purchaseDetailEntity);
            }
        
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                // Fetch the purchase entity
                var purchase = _unitOfWork.PurchaseRepository.FindById(id);
                if (purchase == null || purchase.IsInActive)
                {
                    return false; // If purchase doesn't exist or is inactive, return false
                }
                purchase.IsInActive = true;
                _unitOfWork.PurchaseRepository.Update(purchase);
                var purchaseDetails = _unitOfWork.PurchaseDetailRepository.FindByPurchaseId(id).ToList();

        // Update stock balances for each item in purchase details
                 foreach (var detail in purchaseDetails)
                 {
                 _unitOfWork.StockBalanceRepository.UpdateStockBalanceByItemUnitAndExpiration(
                    detail.Itemid,
                    detail.Unitid,
                    detail.ExpiredDate,
                    -detail.PurchaseQty,
                
                // Decrease quantity since purchase is deleted
                null // You can pass category ID if required
                 );

                    // Remove associated stock ledger entries
                    var stockLedgerEntry = new StockLedgerEntity
                    {
                        id = Guid.NewGuid().ToString(),
                        Itemid = detail.Itemid,
                        Quantity =detail.PurchaseQty, // Negative to indicate reduction
                        Unitid = detail.Unitid,
                        LedgerDate = DateTime.Now,
                        transactionType = "Purchase Delete", // Mark as delete
                        Sourceid = purchase.id,
                        ExpiredDate = detail.ExpiredDate,
                        CreatedAt = DateTime.Now
                    };
                    _unitOfWork.StockLedgerRepository.Create(stockLedgerEntry);
                }

        // Delete purchase details
                _unitOfWork.PurchaseDetailRepository.DeleteRange(purchaseDetails);

                _unitOfWork.Commit();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback(); 
                throw; 
            }
        }

        public IEnumerable<PurchaseViewModel> GetAll(DateTime? fromDate = null, DateTime? toDate = null, string? Supplierid = null, string purchaseid = null)
        {
            var purchase = from p in _unitOfWork.PurchaseRepository.GetAll()
                           join s in _unitOfWork.SupplierRepository.GetAll() on p.Supplierid equals s.id 
                           where !p.IsInActive && !s.IsInActive
                           select new PurchaseViewModel
                           {
                               id= p.id,
                               PurchaseDate=p.PurchaseDate,
                               Supplierid=p.Supplierid,
                               PurchaseVoNo=p.PurchaseVoNO,
                               SupplierName=s.Name,
                               TotalAmount=p.TotalAmount,
                               CashAmount=p.CashAmount,

                           };
            if (fromDate.HasValue)
            {
                purchase = purchase.Where(p => p.PurchaseDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                purchase = purchase.Where(p => p.PurchaseDate <= toDate.Value);
            }

            if (!string.IsNullOrEmpty(Supplierid) && Supplierid != "Select an Supplier")
            {
                purchase = purchase.Where(p => p.Supplierid == Supplierid);
            }

            if (!string.IsNullOrEmpty(purchaseid) && purchaseid != "Select an Voucher No")
            {
                purchase = purchase.Where(p => p.id == purchaseid);
            }

            return purchase.ToList();

        }

        public IEnumerable<PurchaseViewModel> GetPurchases()
        {
          return _unitOfWork.PurchaseRepository.GetPurchases();
        }
    }
}
