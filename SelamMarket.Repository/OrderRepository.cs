using SelamMarket.Data;
using SelamMarket.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelamMarket.Repository
{
    public class OrderRepository : RepositoryBase<Order, long>, IOrderRepository
    {
        public OrderRepository(SelamMarketDbContext marketsDbContext) : base(marketsDbContext)
        {
        }

        public void Create(Order Order)
        {
            _dbSet.Add(Order);
            _dbContext.SaveChanges();
        }

        public List<Order> Read()
        {
            return _dbSet.Where(e => !e.Deleted).ToList();
        }
    }
}
