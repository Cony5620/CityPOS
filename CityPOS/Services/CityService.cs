using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;

namespace CityPOS.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(CityViewModel cityViewModel)
        {
            CityEntity cityEntity = new CityEntity()
            {
                id=Guid.NewGuid().ToString(),
                Name = cityViewModel.Name,
                Remark = cityViewModel.Remark,
                CreatedAt=DateTime.Now,
            };
            _unitOfWork.CityRepository.Create(cityEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {
            try
            {

                CityEntity cityEntity = _unitOfWork.CityRepository.GetBy(w => w.id == id).SingleOrDefault();
                if (cityEntity != null)
                {
                    cityEntity.IsInActive = true;
                    _unitOfWork.CityRepository.Update(cityEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<CityViewModel> GetAll()
        {
            return _unitOfWork.CityRepository.GetAll().Where(w=>!w.IsInActive).Select(s=>new CityViewModel
            {    id=s.id,
                Name=s.Name,
                Remark=s.Remark,

            }).AsEnumerable();
        }

        public CityViewModel GetById(string id)
        {
           return _unitOfWork.CityRepository.GetBy(w=>w.id==id && !w.IsInActive).Select(s=>new CityViewModel
           {
               id = s.id,
               Name = s.Name,
               Remark = s.Remark,
           }).FirstOrDefault();
        }

        public IEnumerable<CityViewModel> GetCities()
        {
            return _unitOfWork.CityRepository.GetCities();
        }

        public bool IsAlreadyExist(CityViewModel cityViewModel)
        {
            return _unitOfWork.CityRepository.IsAlreadyExist(cityViewModel.Name);
        }

        public void Update(CityViewModel cityViewModel)
        {
            CityEntity cityEntity = new CityEntity()
            {
                id = cityViewModel.id,
                Name = cityViewModel.Name,
                Remark = cityViewModel.Remark,
             
            

            };
            _unitOfWork.CityRepository.Update(cityEntity);
            _unitOfWork.Commit();
        }
    }
}
