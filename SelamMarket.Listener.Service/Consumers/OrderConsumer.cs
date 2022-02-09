using SelamMarket.Comman;
using SelamMarket.Listener.Service.Base;
using SelamMarket.Logging.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Listener.Service
{
    public class OrderConsumer : BaseConsumer<Order>
    {
        private IReceiveService _receiveService;

        public OrderConsumer(IReceiveService receiveService, ICoreLogger coreLogger) : base(coreLogger)
        {
            _receiveService = receiveService;
        }

        public override void ConsumeAsync(Order request)
        {
            _receiveService.SaveOrder(request);
        }
    }
}
