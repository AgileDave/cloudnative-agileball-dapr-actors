using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace agileball.subscriber.game
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDaprClient();

            services.AddControllers().AddDapr();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "agileball.subscriber.game", Version = "v1" });
            });

            services.AddAuthorization();

            services.AddSignalR()
                    .AddAzureSignalR();

            var ctx = new ServiceManagerBuilder().WithOptions(opts =>
            {
                opts.ConnectionString = Configuration["Azure:SignalR:ConnectionString"];
                opts.ServiceTransportType = ServiceTransportType.Transient;
            })
            .Build();
            var hub = ctx.CreateHubContextAsync("ChatSampleHub", new LoggerFactory()).Result;
            services.AddSingleton<IServiceHubContext>(hub);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "agileball.subscriber.game v1"));
            }

            //app.UseHttpsRedirection();

            app.UseCloudEvents();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();

                endpoints.MapControllers();
            });
        }
    }
}
