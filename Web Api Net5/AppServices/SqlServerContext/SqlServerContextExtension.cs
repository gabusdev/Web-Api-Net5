﻿using DataEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.MySqlServerContext
{
    public static class SqlServerContextExtension
    {
        public static void ConfigureSqlServerContext(this IServiceCollection services, string conString)
        {
            services.AddDbContext<CoreDbContext>(o =>
                o.UseSqlServer(conString)
            );
        }
    }
}
