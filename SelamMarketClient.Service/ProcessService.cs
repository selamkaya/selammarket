using Newtonsoft.Json;
using SelamMarket.Comman;
using SelamMarket.Comman.Result;
using SelamMarketClient.Service.Base;
using System;
using System.Net.Http;

namespace SelamMarketClient.Service
{
    public class ProcessService : IProcessService
    {
        public void Run()
        {
            try
            {
                Order orderRequest = new Order();

                Console.WriteLine("OrderCode:");
                orderRequest.OrderCode = Console.ReadLine();

                Console.WriteLine("CustomerName:");
                orderRequest.CustomerName = Console.ReadLine();

                Console.WriteLine("Price:");
                orderRequest.Price = Convert.ToDecimal(Console.ReadLine());

                var response = Post<Order, OrderResponse>(orderRequest, "http://localhost:33330/order");

                if (response.Failed)
                {
                    Console.WriteLine($"Error: { response.Message}");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"Success");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: { ex.Message}");
            }

            Console.ReadLine();
        }

        #region Helper
        private DataResult<TRes> Post<TReq, TRes>(TReq request, string url) where TReq : class where TRes : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestString = JsonConvert.SerializeObject(request);

                    var contentData = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");
                    using (HttpResponseMessage res = client.PostAsync(url, contentData).Result)
                    using (HttpContent content = res.Content)
                    {
                        string responseString = content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(responseString)) throw new Exception("RESPONSE-EMPTY");
                        return JsonConvert.DeserializeObject<DataResult<TRes>>(responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                return new DataResult<TRes>()
                {
                    Failed = true,
                    Message = ex.ToString()
                };
            }
        }
        #endregion
    }
}
