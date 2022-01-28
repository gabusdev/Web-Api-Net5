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
    public static class SqlServerService
    {
        public static IServiceCollection AddSqlServerService(this IServiceCollection services, string conString)
        {
            services.AddDbContext<SqlServerDatabaseContext>(o =>
                o.UseSqlServer(conString)
            );
            return services;
        }
    }
}
