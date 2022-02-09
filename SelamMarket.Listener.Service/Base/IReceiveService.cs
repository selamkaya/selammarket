using SelamMarket.Comman;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Listener.Service.Base
{
    public interface IReceiveService
    {
        Task SaveOrder(Order order);
    }
}
