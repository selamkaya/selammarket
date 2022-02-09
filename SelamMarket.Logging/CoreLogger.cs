using Microsoft.Extensions.Logging;
using SelamMarket.Comman;
using SelamMarket.Logging.Base;
using System;
using System.Threading.Tasks;

namespace SelamMarket.Logging
{
    public class CoreLogger : ICoreLogger
    {
        private readonly ILogger _logger;
        public CoreLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Order>();
        }

        public Task Log(string data)
        {
            try
            {
                _logger.LogTrace(data);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "my custom message");
            }
            return Task.CompletedTask;

        }
    }
}