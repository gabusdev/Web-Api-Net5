﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.Authorization
{
    public static class AuthorizationExtension
    {
        public static void ConfigureAuthorization(this IServiceCollection services, bool locked = false)
        {

            services.AddAuthorization(opt =>
            {
                if (locked)
                {
                    opt.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
                }
                opt.AddPolicy("Admin",
                    policy => policy.RequireRole("Admin"));
            });

        }
    }
}
