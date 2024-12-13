using CityPOS.DAO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CityPOS.Repositories.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CityPOSDbContext _dbContext;
        private readonly DbSet<T> _dbSet;   
        public BaseRepository(CityPOSDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbContext.Add<T>(entity);
        }

        public void Delete(T entity)
        {
           _dbContext.Update<T>(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbContext.Update<T>(entity);
        }
    }
}
