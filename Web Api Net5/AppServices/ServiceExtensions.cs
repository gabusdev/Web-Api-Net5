using APICore.API.Middlewares;
using AppServices.ApiVersioning;
using AppServices.Authorization;
using AppServices.Caching;
using AppServices.FluentValidation;
using AppServices.Jwt;
using AppServices.MyCors;
using AppServices.MyIdentity;
using AppServices.MySqlServerContext;
using AppServices.MySwagger;
using AppServices.RateLimit;
using AspNetCoreRateLimit;
using DataEF.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Impl;
using Services.Utils;

namespace Web_Api_Net5.AppServices
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceExtensions(this IServiceCollection services, IConfiguration conf)
        {
            var conString = conf.GetConnectionString("sqlConnection");
            services.ConfigureApiVersioning();
            services.ConfigureAuthorization();
            services.ConfigureCaching();
            services.ConfigureCors();
            services.ConfigureFluentValidation();
            services.ConfigureIdentity();
            services.ConfigureJwt(conf);
            services.ConfigureRateLimit();
            services.ConfigureSqlServerContext(conString);
            services.ConfigureSwagger(conf, true);
            services.AddAuthentication();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers(o =>
            {
                o.CacheProfiles.Add("60SecondsDuraion", new CacheProfile
                {
                    Duration = 60
                });
            });
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            /** For Handling Reference Loops Calls when serializing
            
            services.AddControllers().AddNewtonsoftJson(o =>
                o.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            **/
        }

        public static void UseServiceExtensions(this IApplicationBuilder app)
        {
            app.UseMySwagger();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseCaching();
            app.UseIpRateLimiting();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
