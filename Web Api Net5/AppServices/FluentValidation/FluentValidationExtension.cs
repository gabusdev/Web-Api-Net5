using Common.DTO.Request.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.FluentValidation
{
    public static class FluentValidationExtension
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterDTOValidator>();
        }


        
    }
}
