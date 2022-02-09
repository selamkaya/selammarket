using Microsoft.Extensions.DependencyInjection;
using SelamMarketClient.Service;
using SelamMarketClient.Service.Base;
using System;

namespace SelamMarketClient.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = InitializeServiceProvider();

            var roverService = serviceProvider.GetService<IProcessService>();
            roverService.Run();
        }


        public static IServiceProvider InitializeServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IProcessService, ProcessService>();
            return services.BuildServiceProvider();
        }
    }
}
