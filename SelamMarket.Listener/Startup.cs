using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using NLog.Web.AspNetCore;
using NLog.Extensions.Hosting;
using SelamMarket.Comman;
using SelamMarket.Listener.Service;
using SelamMarket.Listener.Service.Base;
using SelamMarket.Logging;
using SelamMarket.Logging.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using SelamMarket.Repository;
using SelamMarket.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace SelamMarket.Listener
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReceiveService, ReceiveService>();
            services.AddScoped<ICoreLogger, CoreLogger>();

            services.AddDbContext<SelamMarketDbContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Logistics;Trusted_Connection=True;"));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(RabbitMqParam.RabbitMqUri), h =>
                    {
                        h.Username(RabbitMqParam.Username);
                        h.Password(RabbitMqParam.Password);
                    });

                    config.ReceiveEndpoint(RabbitMqParam.Queue, endpoint => endpoint.Consumer<OrderConsumer>(provider));
                }));
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
       

            services.AddHealthChecks();
            services.AddLogging(builder => builder.AddNLog()); ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = Newtonsoft.Json.JsonConvert.SerializeObject(
                            new
                            {
                                Failed = (report.Status != Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy),
                                Message = report.Status.ToString(),
                            });
                    context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}
