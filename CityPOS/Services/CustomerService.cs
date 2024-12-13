using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(CustomerViewModel CustomerViewModel)
        {

            CustomerEntity CustomerEntity = new CustomerEntity()
            {
                id = Guid.NewGuid().ToString(),
                Code = CustomerViewModel.Code,
                Name = CustomerViewModel.Name,
                Email = CustomerViewModel.Email,
                Cityid = CustomerViewModel.Cityid,
                Townshipid = CustomerViewModel.Townshipid,
                Adress= CustomerViewModel.Adress,
                PhoneNo=CustomerViewModel.PhoneNo,
                Company=CustomerViewModel.Company,  
                  CreatedAt = DateTime.Now,

            };
           _unitOfWork.CustomerRepository.Create(CustomerEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {

            try
            {
                CustomerEntity CustomerEntity = _unitOfWork.CustomerRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (CustomerEntity != null)
                {
                    CustomerEntity.IsInActive = true;
                    _unitOfWork.CustomerRepository.Update(CustomerEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
           IEnumerable<CustomerViewModel>Customers= (from c in _unitOfWork.CustomerRepository.GetAll()
                                            join ct in _unitOfWork.CityRepository.GetAll()
                                            on c.Cityid equals ct.id
                                            join t in _unitOfWork.TownshipRepository.GetAll()
                                             on c.Townshipid equals t.id
                                             where !c.IsInActive && !ct.IsInActive && !t.IsInActive
                                            select new CustomerViewModel
                                            {
                                                id = c.id,
                                                Code = c.Code,
                                                Name = c.Name,
                                               Email = c.Email,
                                               Cityid = c.Cityid,
                                               CityName=ct.Name,
                                               Townshipid=c.Townshipid,
                                               TownshipName=t.Name,
                                               Adress=c.Adress,
                                               PhoneNo=c.PhoneNo,
                                               Company=c.Company,

                                            }).ToList();
            return Customers;
        }

        public IEnumerable<CustomerViewModel> GetCustomers()
        {
            return _unitOfWork.CustomerRepository.GetCustomers();
            
        }

        public CustomerViewModel GetById(string id)
        {
           return _unitOfWork.CustomerRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s => new CustomerViewModel
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

        public void Update(CustomerViewModel CustomerViewModel)
        {
            CustomerEntity CustomerEntity = new CustomerEntity()
            {
                id =CustomerViewModel.id,
                Code = CustomerViewModel.Code,
                Name = CustomerViewModel.Name,
                Email = CustomerViewModel.Email,
                Cityid = CustomerViewModel.Cityid,
                Townshipid = CustomerViewModel.Townshipid,
                Adress = CustomerViewModel.Adress,
                PhoneNo = CustomerViewModel.PhoneNo,
                Company = CustomerViewModel.Company,
                CreatedAt = CustomerViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,

            };
            _unitOfWork.CustomerRepository.Update(CustomerEntity);
            _unitOfWork.Commit();
        }

        public bool IsAlreadyExist(CustomerViewModel CustomerViewModel)
        {
            return _unitOfWork.CustomerRepository.IsAlreadyExist(CustomerViewModel.Code, CustomerViewModel.Name);
        }

        public string GetNextCustomerCode()
        {
            var lastCode = _unitOfWork.CustomerRepository.GetNextCustomerCode();
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
