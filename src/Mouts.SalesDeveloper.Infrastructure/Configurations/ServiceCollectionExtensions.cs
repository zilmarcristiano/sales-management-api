using Mouts.SalesDeveloper.Domain.DomainValidation;
using Mouts.SalesDeveloper.Domain.Entities;
using Mouts.SalesDeveloper.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mouts.SalesDeveloper.Infrastructure.Data;
using Mouts.SalesDeveloper.Infrastructure.Data.Repositories;

namespace Mouts.SalesDeveloper.Infrastructure.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Sale>, SaleValidator>();
            services.AddScoped<IValidator<SaleItem>, SaleItemValidator>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DefaultContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            return services;
        }
    }
}
