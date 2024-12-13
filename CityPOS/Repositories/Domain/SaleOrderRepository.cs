using CityPOS.DAO;
using CityPOS.Models.DataModels;
using CityPOS.Repositories.Common;

namespace CityPOS.Repositories.Domain
{
    public class SaleOrderRepository : BaseRepository<SaleOrderEntity>, ISaleOrderRepository
    {
        public SaleOrderRepository(CityPOSDbContext dbContext) : base(dbContext)
        {
        }
    }
}
