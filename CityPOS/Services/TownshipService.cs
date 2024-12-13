using CityPOS.Models.DataModels;
using CityPOS.Models.ViewModels;
using CityPOS.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CityPOS.Services
{
    public class TownshipService : ITownshipService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TownshipService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void Create(TownshipViewModel townshipViewModel)
        {
            TownshipEntity townshipEntity = new TownshipEntity()
            {
                id = Guid.NewGuid().ToString(),
                Name = townshipViewModel.Name,
                Cityid=townshipViewModel.Cityid,
                DeliveryFees = townshipViewModel.DeliveryFees,
                CreatedAt = DateTime.Now,

            };
            _unitOfWork.TownshipRepository.Create(townshipEntity);
            _unitOfWork.Commit();
        }

        public bool Delete(string id)
        {
            try
            {
                TownshipEntity townshipEntity = _unitOfWork.TownshipRepository.GetBy(w => w.id == id).SingleOrDefault();
                if (townshipEntity != null)
                {
                    townshipEntity.IsInActive = true;
                    _unitOfWork.TownshipRepository.Update(townshipEntity);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public IEnumerable<TownshipViewModel> GetAll()
        {
          IEnumerable<TownshipViewModel>township =(from t in _unitOfWork.TownshipRepository.GetAll()
                                                   join ct in _unitOfWork.CityRepository.GetAll()
                                                   on t.Cityid equals ct.id 
                                                   where !t.IsInActive && !ct.IsInActive 
                                                   select new  TownshipViewModel
                                                   {
                                                          id=t.id,
                                                          Name=t.Name,
                                                          Cityid=t.Cityid,
                                                          City=ct.Name,
                                                          DeliveryFees=t.DeliveryFees,
                                                          CreatedAt = t.CreatedAt,
                                                          ModifiedAt = t.ModifiedAt,
                                                   }).AsEnumerable();
            return township;
        }

        public TownshipViewModel GetById(string id)
        {
            return _unitOfWork.TownshipRepository.GetBy(w => w.id == id && !w.IsInActive).Select(s => new TownshipViewModel
            {
                id = s.id,
                Name = s.Name,
                Cityid = s.Cityid,
                DeliveryFees = s.DeliveryFees,
                CreatedAt = s.CreatedAt,
                ModifiedAt = s.ModifiedAt,
            }).SingleOrDefault();
        }

        public IEnumerable<TownshipViewModel> GetTownships()
        {
            return _unitOfWork.TownshipRepository.GetTownships();
        }

        public IEnumerable<TownshipViewModel> GetTownshipsByCityId(string cityId)
        {
            return _unitOfWork.TownshipRepository.GetTownshipsByCityId(cityId);
        }

        public bool IsAlreadyExist(TownshipViewModel townshipViewModel)
        {
           return _unitOfWork.TownshipRepository.IsAlreadyExist(townshipViewModel.Name);
        }

        public void Update(TownshipViewModel townshipViewModel)
        {
            TownshipEntity townshipEntity = new TownshipEntity()
            {
                id =townshipViewModel.id,
                Name = townshipViewModel.Name,
                Cityid = townshipViewModel.Cityid,
                DeliveryFees = townshipViewModel.DeliveryFees,
                CreatedAt = DateTime.Now,

            };
            _unitOfWork.TownshipRepository.Update(townshipEntity);
            _unitOfWork.Commit();
        }
    }
}
