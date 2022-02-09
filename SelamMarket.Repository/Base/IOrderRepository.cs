using SelamMarket.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamMarket.Repository.Base
{
	public partial interface IOrderRepository : IRepository<Order, long>
	{
		void Create(Order Order);

		List<Order> Read();
	}
}
