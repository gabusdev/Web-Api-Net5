using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MySwaggerExtensions
{
    public static class MySwaggerJwtExtensions
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services, IConfiguration config, bool JwtAuth = false)
        {
            var SwaggerConfig = GetConfig(config);
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Description = SwaggerConfig.Description,
                    Title = SwaggerConfig.Title,
                    Version = SwaggerConfig.Version,
                    Contact = new OpenApiContact()
                    {
                        Name = SwaggerConfig.Contact_Name,
                        Url = new Uri(SwaggerConfig.Contact_Url)
                    }
                });
                if (JwtAuth)
                {
                    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                    });
                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                    });
                }
                
            });
            return services;
        }

        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }

        private static MySwaggerConfig GetConfig(IConfiguration config)
        {
            return new MySwaggerConfig
            {
                Title = config["MySwagger:Title"] ?? "Default Title",
                Description = config["MySwagger:Description"] ?? "Default API Demo",
                Version = config["MySwagger:Version"] ?? "v1",
                Contact_Name = config["MySwagger:Contact_Name"] ?? "Default Name",
                Contact_Url = config["MySwagger:Contact_Url"] ?? "https://github.com/gabusdev"
            };
        }
    }
}
