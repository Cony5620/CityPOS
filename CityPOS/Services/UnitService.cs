using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(UnitViewModel unitViewModel)
        {
            UnitEntity unitEntity = new UnitEntity()
            {
                id = Guid.NewGuid().ToString(),
                Name = unitViewModel.Name,
                Remark = unitViewModel.Remark,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.UnitRepository.Create(unitEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {
            try
            {
                UnitEntity unitEntity = _unitOfWork.UnitRepository.GetBy(w => id == w.id).SingleOrDefault();
                if (unitEntity != null)
                {
                    unitEntity.IsInActive = true;
                    _unitOfWork.UnitRepository.Update(unitEntity);
                    _unitOfWork.Commit();

                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<UnitViewModel> GetAll()
        {

           return _unitOfWork.UnitRepository.GetAll().Where(w => !w.IsInActive).Select(s => new UnitViewModel
            {
                id = s.id,
                Name = s.Name,
                Remark = s.Remark,
            }).AsEnumerable();
        }

        public UnitViewModel GetBy(string id)
        {
             return  _unitOfWork.UnitRepository.GetBy(w => !w.IsInActive).Select(s => new UnitViewModel
            {
                id = s.id,
                Name = s.Name,
                Remark = s.Remark,
                CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).FirstOrDefault();
        }

        public IEnumerable<UnitViewModel> GetUnits()
        {
            return _unitOfWork.UnitRepository.GetUnits();
        }

        public bool IsAlreadyExist(UnitViewModel unitViewModel)
        {
          return _unitOfWork.UnitRepository.IsAlreadyExist(unitViewModel.Name);
        }

        public void Update(UnitViewModel unitViewModel)
        {

            UnitEntity unitEntity = new UnitEntity()
            {
                id = unitViewModel.id,
                Name = unitViewModel.Name,
                Remark = unitViewModel.Remark,
                CreatedAt = unitViewModel.CreatedAt,
                ModifiedAt = DateTime.Now,
            };
            _unitOfWork.UnitRepository.Update(unitEntity);
            _unitOfWork.Commit();
        }
    }
}
