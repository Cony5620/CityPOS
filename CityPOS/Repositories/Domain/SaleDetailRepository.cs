using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class SaleDetailRepository:BaseRepository<SaleDetatilEntity>,ISaleDetailRepository
    {
        public SaleDetailRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
