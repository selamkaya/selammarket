using SelamMarket.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Data
{
    public class Order : EntityBase<long>
    {
        public string OrderCode { get; set; }

        public string CustomerName { get; set; }

        public decimal Price { get; set; }
    }
}
