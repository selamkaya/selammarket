using MassTransit;
using SelamMarket.Comman;
using SelamMarket.Comman.Result;
using SelamMarket.Publisher.Service.Base;
using System;
using System.Threading.Tasks;

namespace SelamMarket.Publisher.Service
{
    public class SenderService : ISenderService
    {
        private IBusControl _busControl;
        public SenderService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task<DataResult<bool>> SendOrder(Order order)
        {
            DataResult<bool> dataResult = new DataResult<bool>();

            try
            {
                Uri uri = new Uri(RabbitMqParam.RabbitMqUri);
                var endPoint = await _busControl.GetSendEndpoint(new Uri($"{RabbitMqParam.RabbitMqUri}/{RabbitMqParam.Queue}"));
                await endPoint.Send(order);

                await Task.FromResult(0);
            }
            catch (Exception ex)
            {
                dataResult.Failed = true;
                dataResult.Title = "Send Error";
                dataResult.Message = ex.Message;
            }

            return dataResult;
        }
    }
}
