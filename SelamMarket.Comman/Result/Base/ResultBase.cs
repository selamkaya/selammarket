using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Comman.Result.Base
{
    public abstract class ResultBase
    {
        public bool Failed { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
