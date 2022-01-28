using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreEF
{
    public static class Service
    {
        public static IServiceCollection AddSqlServerService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<HotelsContext>(o =>
                o.UseSqlServer(config.GetConnectionString("sqlConnection"))
            );
            return services;
        }
    }
}
