using AppServices.MyCors;
using AppServices.MySqlServerContext;
using AppServices.MySwagger;
using AppServices.MyIdentity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web_Api_Net5.Repository;
using Web_Api_Net5.Utils;
using Microsoft.AspNetCore.Builder;
using AppServices.Jwt;
using Web_Api_Net5.Services;
using Web_Api_Net5.Services.Impl;

namespace Web_Api_Net5.AppServices
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceExtensions(this IServiceCollection services, IConfiguration conf)
        {
            var conString = conf.GetConnectionString("sqlConnection");
            services.ConfigureSqlServerContext(conString);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.ConfigureCors();
            services.AddAuthentication();
            services.ConfigureJwt(conf);
            services.ConfigureIdentity();
            services.AddControllers();
            services.ConfigureSwagger(conf, true);
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
            app.UseMyCors();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            

        }
    }
}
