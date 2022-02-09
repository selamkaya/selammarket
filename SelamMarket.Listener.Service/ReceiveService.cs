using SelamMarket.Comman;
using SelamMarket.Listener.Service.Base;
using SelamMarket.Repository.Base;
using System;
using System.Threading.Tasks;

namespace SelamMarket.Listener.Service
{
    public class ReceiveService : IReceiveService
    {
        private IOrderRepository _orderRepository;
        public ReceiveService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task SaveOrder(Order order)
        {
            try
            {
                //use automapper
                Data.Order orderEntity = new Data.Order()
                {
                    CustomerName = order.CustomerName,
                    Price = order.Price,
                    OrderCode = order.OrderCode,
                    CreatedUserId = 1
                };
                //_orderRepository.Create(orderEntity);
            }
            catch (Exception ex)
            {
                //add exeption log
            }
        }
    }
}
