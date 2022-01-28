using DataStoreEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.MySqlServerContext
{
    public static class MySqlServerContext
    {
        public static IServiceCollection AddMySqlServerContext(this IServiceCollection services, string conString)
        {
            services.AddDbContext<HotelsContext>(o =>
                o.UseSqlServer(conString)
            );
            return services;
        }
    }
}
