using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Persistence;
using PackageTracking.Infrastructure.Repository;
using PackageTracking.Infrastructure.Seeders;
using PackageTracking.Infrastructure.Seeders.Interfaces;

namespace PackageTracking.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PackageTrackingDbContext>(options => options.UseMySql(connectionString,
            new MySqlServerVersion(new Version(10, 4, 22))).EnableSensitiveDataLogging());

        services.AddScoped<IReceiverSeeder, ReceiverSeeder>();
        services.AddScoped<IReceiverRepository, ReceiverRepository>();
        services.AddScoped<IPackageRepository, PackageRepository>();
    }
}
