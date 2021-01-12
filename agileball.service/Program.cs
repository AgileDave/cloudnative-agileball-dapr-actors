using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agileball.service.actors;
using Dapr.Actors.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace agileball.service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseActors(rt =>
                        {
                            rt.RegisterActor<GameEvent>();
                            rt.RegisterActor<GameSupervisor>();
                            rt.RegisterActor<Batter>();
                            rt.RegisterActor<Team>();
                        });
                    //.UseUrls("http://localhost:5000");
                });
    }
}
