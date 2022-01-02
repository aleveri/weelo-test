using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RealState.Common.Interfaces;
using RealState.Common.Interfaces.Data;
using RealState.Common.Interfaces.Services;
using RealState.Common.Models;
using RealState.Data.Contexts;
using RealState.Data.Repositories;
using RealState.Services;
using System;

namespace RealState
{
    public class Startup
    {
        readonly string SpecificOrigins = "_specificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealState", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: SpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                      .WithMethods("PUT", "DELETE", "GET", "POST");
                                  });
            });

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddDbContext<SqlServerContext>();

            services.AddTransient<IResponse, Response>();

            services.AddScoped(typeof(IGenericSqlServerRepository<>), typeof(GenericSqlServerRepository<>));

            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealState v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                context.Response.Headers.Add("Feature-Policy",
                    "accelerometer 'none'; camera 'none'; geolocation 'none'; microphone 'none'; usb 'none'");
                await next();
            });

            app.UseCsp(opts => opts
                .BlockAllMixedContent()
                .DefaultSources(s => s.Self())
                .ScriptSources(s => s.Self())
                .StyleSources(s => s.Self())
                .FontSources(s => s.Self())
            );
            app.UseXContentTypeOptions();
            app.UseHttpsRedirection();
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(options => options.NoReferrer());
            app.UseXfo(options => options.Deny());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
