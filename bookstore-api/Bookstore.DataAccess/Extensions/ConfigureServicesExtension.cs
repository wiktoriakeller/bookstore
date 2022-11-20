using Bookstore.DataAccess.Repositories;
using Bookstore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.DataAccess.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookstoreDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ConfigureServicesExtension).Assembly.FullName)));

            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IPublishersRepository, PublishersRepository>();
            services.AddScoped<IGenresRepository, GenresRepository>();
            return services;
        }
    }
}
