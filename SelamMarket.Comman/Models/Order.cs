using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Comman
{
    public class Order
    {
        public long Id { get; set; }

        public string OrderCode { get; set; }

        public string CustomerName { get; set; }

        public decimal Price { get; set; }
    }
}
