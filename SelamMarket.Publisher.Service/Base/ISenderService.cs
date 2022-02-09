using SelamMarket.Comman;
using SelamMarket.Comman.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Publisher.Service.Base
{
    public interface ISenderService
    {
        Task<DataResult<bool>> SendOrder(Order order);
    }
}
