using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(SupplierViewModel SupplierViewModel)
        {

            SupplierEntity SupplierEntity = new SupplierEntity()
            {
                id = Guid.NewGuid().ToString(),
                Code = SupplierViewModel.Code,
                Name = SupplierViewModel.Name,
                Email = SupplierViewModel.Email,
                Cityid = SupplierViewModel.Cityid,
                Townshipid = SupplierViewModel.Townshipid,
                Adress= SupplierViewModel.Adress,
                PhoneNo=SupplierViewModel.PhoneNo,
                Company=SupplierViewModel.Company,  
                  CreatedAt = DateTime.Now,

            };
           _unitOfWork.SupplierRepository.Create(SupplierEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {

            try
            {
                SupplierEntity SupplierEntity = _unitOfWork.SupplierRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (SupplierEntity != null)
                {
                    SupplierEntity.IsInActive = true;
                    _unitOfWork.SupplierRepository.Update(SupplierEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<SupplierViewModel> GetAll()
        {
           IEnumerable<SupplierViewModel>Suppliers= (from s in _unitOfWork.SupplierRepository.GetAll()
                                            join ct in _unitOfWork.CityRepository.GetAll()
                                            on s.Cityid equals ct.id
                                            join t in _unitOfWork.TownshipRepository.GetAll()
                                             on s.Townshipid equals t.id
                                             where !s.IsInActive && !ct.IsInActive && !t.IsInActive
                                            select new SupplierViewModel
                                            {
                                                id = s.id,
                                                Code = s.Code,
                                                Name = s.Name,
                                               Email = s.Email,
                                               Cityid = s.Cityid,
                                               CityName=ct.Name,
                                               Townshipid=s.Townshipid,
                                               TownshipName=t.Name,
                                               Adress=s.Adress,
                                               PhoneNo=s.PhoneNo,
                                               Company=s.Company,

                                            }).ToList();
            return Suppliers;
        }

        public IEnumerable<SupplierViewModel> GetSuppliers()
        {
            return _unitOfWork.SupplierRepository.GetSuppliers();
            
        }

        public SupplierViewModel GetById(string id)
        {
           return _unitOfWork.SupplierRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s => new SupplierViewModel
            {

               id = s.id,
               Code = s.Code,
               Name = s.Name,
               Email = s.Email,
               Cityid = s.Cityid,
               Townshipid = s.Townshipid,
               Adress = s.Adress,
               PhoneNo = s.PhoneNo,
               Company = s.Company,
               CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        }

        public void Update(SupplierViewModel SupplierViewModel)
        {
            SupplierEntity SupplierEntity = new SupplierEntity()
            {
                id =SupplierViewModel.id,
                Code = SupplierViewModel.Code,
                Name = SupplierViewModel.Name,
                Email = SupplierViewModel.Email,
                Cityid = SupplierViewModel.Cityid,
                Townshipid = SupplierViewModel.Townshipid,
                Adress = SupplierViewModel.Adress,
                PhoneNo = SupplierViewModel.PhoneNo,
                Company = SupplierViewModel.Company,
                CreatedAt = SupplierViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,

            };
            _unitOfWork.SupplierRepository.Update(SupplierEntity);
            _unitOfWork.Commit();
        }

        public bool IsAlreadyExist(SupplierViewModel SupplierViewModel)
        {
            return _unitOfWork.SupplierRepository.IsAlreadyExist(SupplierViewModel.Code, SupplierViewModel.Name);
        }

        public string GetNextSupplierCode()
        {
            var lastCode = _unitOfWork.SupplierRepository.GetNextSupplierCode();
            if (lastCode != null) 
            {
             int newCode = int.Parse(lastCode) + 1;
                return newCode.ToString("D3");
            }
            else
            {
                return "001";
            }
        }

      
    }
}
