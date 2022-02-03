using AppServices.MyCors;
using AppServices.MySqlServerContext;
using AppServices.MySwagger;
using AppServices.MyIdentity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using AppServices.Jwt;
using DataEF.UnitOfWork;
using APICore.API.Middlewares;
using AppServices.Authorization;
using Services.Utils;
using Services;
using Services.Impl;
using AppServices.FluentValidation;

namespace Web_Api_Net5.AppServices
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceExtensions(this IServiceCollection services, IConfiguration conf)
        {
            var conString = conf.GetConnectionString("sqlConnection");
            services.ConfigureAuthorization();
            services.ConfigureCors();
            services.ConfigureFluentValidation();
            services.ConfigureIdentity();
            services.ConfigureJwt(conf);
            services.ConfigureSqlServerContext(conString);
            services.ConfigureSwagger(conf, true);
            services.AddAuthentication();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManager, AuthManager>();
            
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
