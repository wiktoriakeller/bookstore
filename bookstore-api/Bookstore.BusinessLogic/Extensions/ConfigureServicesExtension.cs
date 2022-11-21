using Bookstore.BusinessLogic.Services;
using Bookstore.Interfaces.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.BusinessLogic.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IPublishersService, PublishersService>();

            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(typeof(ConfigureServicesExtension).Assembly);
            services.AddValidatorsFromAssembly(typeof(ConfigureServicesExtension).Assembly);
            return services;
        }
    }
}
