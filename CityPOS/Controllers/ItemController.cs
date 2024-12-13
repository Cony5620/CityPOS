using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _ItemService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly ISupplierService _supplierService;
        private readonly IUnitService _unitService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(IItemService ItemService,ICategoryService categoryService,IBrandService brandService,ISupplierService supplierService,IUnitService unitService, IWebHostEnvironment webHostEnvironment)
        {
            this._ItemService = ItemService;
            this._categoryService = categoryService;
            this._brandService = brandService;
            this._supplierService = supplierService;
            this._unitService = unitService;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetBrand(string categoryid)
        {


            var brands = _brandService.GetBrandsByCategory(categoryid); 
            return Json(brands);
        }
      
        [HttpGet ]
        public IActionResult Entry()
        {

            string nextCode = _ItemService.GetNextItemCode();

            var itemViewModel = new ItemViewModel
            {
                Code = nextCode,
                Name = string.Empty,
                Categoryid = string.Empty,
                CategoryName = string.Empty,
                Brandid = string.Empty,
                BrandName = string.Empty,
                Supplierid = string.Empty,
                SupplierName = string.Empty,
                photo = string.Empty,
                FDANo=string.Empty,
                BarCode = string.Empty,
               
            };
            var priceViewModel = new PriceViewModel
            {PurchasePrice=0.00m,
            RetailSalePrice=0.00m,
            WholeSalePrice=0.00m,

            };

            ViewData["Info"] = null;
            ViewData["Status"] = null;
            bindCategoryData();
            bindBrandData();
            bindSupplierData();
            bindUnitData();

            return View(Tuple.Create(itemViewModel, priceViewModel));
        }

        private void bindUnitData()
        {
            var unit = _unitService.GetUnits();
            ViewBag.Unit = unit;    
        }

        private void bindSupplierData()
        {
            var supplier=_supplierService.GetSuppliers();
            ViewBag.Supplier = supplier;
        }

        private void bindBrandData()
        {
          var brand=_brandService.GetBrands();
            ViewBag.Brand = brand;
        }

        private void bindCategoryData()
        {
           var category=_categoryService.GetCategories();
            ViewBag.Category = category;
        }
        private string UploadedFile(ItemViewModel itemViewModel)
        {
            string uniqueFileName = null;

            if (itemViewModel.photoimg != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + itemViewModel.photoimg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    itemViewModel.photoimg.CopyTo(fileStream);
                }
            }
            return  uniqueFileName;
        }
            [HttpPost]
            public IActionResult Entry(ItemViewModel ItemViewModel, PriceViewModel priceViewModel)
        {
            try
            {
                var isAlreadyExist = _ItemService.IsAlreadyExist(ItemViewModel);
                if (isAlreadyExist)
                {
                    ViewData["Info"] = "This item code or name is already exist in the system.";
                    ViewData["Status"] = false;
                    bindCategoryData();
                    bindBrandData();
                    bindSupplierData();
                    bindUnitData();
                    return View(Tuple.Create(ItemViewModel, priceViewModel));
                }
                string uniqueFileName= UploadedFile(ItemViewModel);
                ItemViewModel.photo = uniqueFileName;
                _ItemService.Create(ItemViewModel,priceViewModel,uniqueFileName);
                ViewData["Info"] = "Successfully save data to the system";
                ViewData["status"] = true;
            }
            catch (Exception e)
            {

                ViewData["Info"] = "Error occur when saving data to the system" + e.Message;
                ViewData["status"] = false;
            }
            bindCategoryData();
            bindBrandData();
            bindSupplierData();
            bindUnitData();
            return View(Tuple.Create(ItemViewModel, priceViewModel));
        }
        public IActionResult List()
        {
           
            var Items= _ItemService.GetAll();
            return View(Items);
        }
        public IActionResult Edit(string id)
        {var ItemView=_ItemService.GetById(id);

            bindCategoryData();
            bindBrandData();
            bindSupplierData();
            bindUnitData();
            return View(ItemView);
        }
        [HttpPost]
        public IActionResult Update(ItemViewModel ItemViewModel, PriceViewModel priceViewModel)
        {
            try
            {
                string uniqueFileName = UploadedFile(ItemViewModel);
                ItemViewModel.photo = uniqueFileName;
                _ItemService.Update(ItemViewModel, uniqueFileName);
                TempData["Info"] = "Successfully update data to the system";
                TempData["status"] = true;
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when updateing data to the system" + e.Message;
                TempData["status"] = false;
            }
            return RedirectToAction("List");
        }
        public IActionResult Delete(string id)
        {
            try
            {
                _ItemService.Delete(id);
                TempData["Info"] = "Successfully delete data to the system";
                    TempData["status"] = true;
                
            }
            catch (Exception e)
            {

                TempData["Info"] = "Error occur when deleting data to the system" + e.Message;
                TempData["status"] = false;
            }
            return RedirectToAction("List");
        }
    }
}
