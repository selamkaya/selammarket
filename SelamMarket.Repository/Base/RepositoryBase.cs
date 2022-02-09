using Microsoft.EntityFrameworkCore;
using SelamMarket.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Repository.Base
{
    public abstract class RepositoryBase<TEntity, TId> where TEntity : EntityBase<TId>
    {
        protected readonly SelamMarketDbContext _dbContext;

        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(SelamMarketDbContext marketsDbContext)
        {
            _dbContext = marketsDbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity ReadById(TId id)
        {
            return _dbSet.FirstOrDefault(e => e.Id.Equals(id) && !e.Deleted);
        }

        public async Task<TEntity> ReadByIdAsync(TId id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.Deleted);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
