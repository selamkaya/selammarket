using MassTransit;
using Newtonsoft.Json;
using SelamMarket.Logging.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SelamMarket.Listener.Service.Base
{
    public abstract class BaseConsumer<TRequest> : IConsumer<TRequest> where TRequest : class
    {
        private ICoreLogger _coreLogger;
        public BaseConsumer(ICoreLogger coreLogger)
        {
            _coreLogger = coreLogger;
        }

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            TRequest requestBody = context.Message;
            string requestLogData = SerializeAndIndentJsonIfNeeded(requestBody);
            await _coreLogger.Log(requestLogData);

            ConsumeAsync(context.Message);
        }

        public abstract void ConsumeAsync(TRequest request);

        private string SerializeAndIndentJsonIfNeeded(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
