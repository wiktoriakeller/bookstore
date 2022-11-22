using Bookstore.Core.Dtos.Books;
using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.ApiInterfaces;
using Bookstore.UI.Common.Validators;
using Bookstore.UI.Common.Validators.Publishers;
using FluentValidation;
using MudBlazor.Services;
using Refit;

namespace Bookstore.UI.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddUILayer(this IServiceCollection services)
        {
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.MaxDisplayedSnackbars = 4;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 1500;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
            });

            AddHttpClients(services);
            AddValidators(services);
            return services;
        }

        private static void AddHttpClients(IServiceCollection services)
        {
            var apiUrl = "https://localhost:44361";
            services.AddRefitClient<IPublishersApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));
            services.AddRefitClient<IBooksApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl));
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IFormValidator<AddPublisherDto>, AddPublisherDtoValidator>();
            services.AddScoped<IFormValidator<UpdatePublisherDto>, UpdatePublisherDtoValidator>();
            services.AddScoped<IFormValidator<AddBookDto>, AddBookDtoValidator>();
            services.AddScoped<IFormValidator<UpdateBookDto>, UpdateBookDtoValidator>();
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}
