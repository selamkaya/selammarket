using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SelamMarket.Comman;
using SelamMarket.Logging;
using SelamMarket.Logging.Base;
using SelamMarket.Publisher.Service;
using SelamMarket.Publisher.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelamMarket.Publisher
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
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(RabbitMqParam.RabbitMqUri), h =>
                    {
                        h.Username(RabbitMqParam.Username);
                        h.Password(RabbitMqParam.Password);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddScoped<ISenderService, SenderService>();
            //services.AddScoped<ICoreLogger, CoreLogger>();

            services.AddHealthChecks();
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
                ResponseWriter = async (context, report) => {
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
