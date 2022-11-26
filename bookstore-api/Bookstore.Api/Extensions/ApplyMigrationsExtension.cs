using Microsoft.EntityFrameworkCore;

namespace Bookstore.DataAccess.Extensions
{
    public static class ApplyMigrationsExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            var useMockDataAccess = app.Configuration.GetValue<bool>("DataAccess:UseMockDataAccess");
            if (!useMockDataAccess)
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BookstoreDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
