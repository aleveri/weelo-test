using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealState.Data.Contexts;
using RealState.Data.Initializers;
using System;

namespace RealState
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            try
            {
                using IServiceScope scope = host.Services.CreateScope();
                IServiceProvider services = scope.ServiceProvider;
                var context = services.GetRequiredService<SqlServerContext>();
                SqlServerInitializer.Initialize(context);
            }
            catch (Exception e)
            {
                //Log error
            }
        }
    }
}
