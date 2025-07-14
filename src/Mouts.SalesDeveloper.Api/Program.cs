using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mouts.SalesDeveloper.Api.Middleware;
using Mouts.SalesDeveloper.Application.Sales.Commands.Handlers;
using Mouts.SalesDeveloper.Domain.Validation;
using Mouts.SalesDeveloper.Infrastructure.Configurations;
using Mouts.SalesDeveloper.Infrastructure.Data;
using System.IO;

namespace Mouts.SalesDeveloper.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateApp(args);
            app.Run();
        }

        public static WebApplication CreateApp(string[]? args = null)
        {
            var builder = WebApplication.CreateBuilder(args ?? []);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(DefaultContext).Assembly.GetName().Name!)
                )
            );

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(Mouts.SalesDeveloper.Application.Sales.Mappings.SaleProfile).Assembly);

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(
                    typeof(Program).Assembly,
                    typeof(CreateSaleHandler).Assembly
                )
            );

            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"));

            builder.WebHost.UseUrls("http://+:5000");

            var app = builder.Build();

            app.UseMiddleware<DomainExceptionMiddleware>();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.MapHealthChecks("/health");

            var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Migration");

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            logger.LogInformation("ConnectionString atual: {ConnectionString}", connectionString);

            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
            {
                MigrationHandler.ApplyMigrations(app.Services, logger);
            }

            return app;
        }
    }
}
