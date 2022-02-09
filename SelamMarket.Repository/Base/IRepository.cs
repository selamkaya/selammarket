using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Repository.Base
{
    public interface IRepository<TEntity, TId>
    {
        TEntity ReadById(TId id);

        void Update(TEntity entity);
    }
}
