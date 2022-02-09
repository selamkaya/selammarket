using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Logging.Base
{
    public interface ICoreLogger
    {
        Task Log(string data);
    }
}