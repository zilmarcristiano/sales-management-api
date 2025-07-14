using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mouts.SalesDeveloper.Infrastructure.Data;

namespace Mouts.SalesDeveloper.Infrastructure.Configurations
{
    public static class MigrationHandler
    {
        public static void ApplyMigrations(IServiceProvider serviceProvider, ILogger logger)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

            var pendingMigrations = context.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Aplicando migrations pendentes...");
                context.Database.Migrate();
            }
            else
            {
                logger.LogInformation("Nenhuma migration pendente encontrada.");
            }
        }
    }
}
