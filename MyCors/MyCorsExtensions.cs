using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MyCors
{
    public static class MyCorsExtensions
    {
        public static IServiceCollection AddMyCorsExtensions(this IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("MyCorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                 );
            });
            return services;
        }

        public static IApplicationBuilder UseMyCorsExtensions(this IApplicationBuilder app)
        {
            app.UseCors("MyCorsPolicy");
            return app;
        }
    }
}
