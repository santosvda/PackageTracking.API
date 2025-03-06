using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Interfaces;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Authorization;
using PackageTracking.Infrastructure.Authorization.Requirements;
using PackageTracking.Infrastructure.Authorization.Services;
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

        services.AddIdentityApiEndpoints<User>(options => { })
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<PackageTrackingUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<PackageTrackingDbContext>();

        services.AddScoped<IReceiverSeeder, ReceiverSeeder>();
        services.AddScoped<IReceiverRepository, ReceiverRepository>();
        services.AddScoped<IPackageRepository, PackageRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality
            , builder => builder.RequireClaim(AppClaimTypes.Nationality, "Brazilian", "American"))
            .AddPolicy(PolicyNames.AtLeast20
            , builder => builder.AddRequirements(new MinimumAgeRequirement(20)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IPackagesAuthorizationService, PackagesAuthorizationService>();

    }
}
