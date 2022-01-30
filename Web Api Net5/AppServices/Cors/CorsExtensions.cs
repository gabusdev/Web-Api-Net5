using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.MyCors
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("MyCorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                 );
            });
            //return services;
        }

        public static IApplicationBuilder UseMyCors(this IApplicationBuilder app)
        {
            app.UseCors("MyCorsPolicy");
            return app;
        }
    }
}
