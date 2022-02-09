using SelamMarket.Comman.Result.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Comman.Result
{
    public class DataResult<TData> : ResultBase
    {
        public TData Data { get; set; }
    }
}
