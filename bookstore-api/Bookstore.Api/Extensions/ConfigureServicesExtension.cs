using Bookstore.BusinessLogic.Services;
using Bookstore.DataAccess;
using Bookstore.DataAccess.Repositories;
using Bookstore.DataAccessMock.Repositories;
using Bookstore.Interfaces.Repositories;
using Bookstore.Interfaces.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Api.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddDataAccess(services, configuration);
            AddBusinessLogic(services);
            return services;
        }

        private static void AddBusinessLogic(IServiceCollection services)
        {
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<IBooksService, BooksService>();

            services.AddFluentValidationAutoValidation();
            services.AddAutoMapper(typeof(PublishersService).Assembly);
            services.AddValidatorsFromAssembly(typeof(PublishersService).Assembly);
        }

        private static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var useMockDataAccess = configuration.GetValue<bool>("DataAccess:UseMockDataAccess");

            if (useMockDataAccess)
            {
                services.AddScoped<IBooksRepository, BooksRepositoryMock>();
                services.AddScoped<IPublishersRepository, PublishersRepositoryMock>();
            }
            else
            {
                services.AddDbContext<BookstoreDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(BooksRepository).Assembly.FullName)));

                services.AddScoped<IBooksRepository, BooksRepository>();
                services.AddScoped<IPublishersRepository, PublishersRepository>();
            }
        }
    }
}
