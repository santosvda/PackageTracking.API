using PackageTracking.Domain.Entities;
using PackageTracking.Infrastructure.Authorization;

namespace PackageTracking.Domain.Interfaces;

public interface IPackagesAuthorizationService
{
    bool Authorize(Package package, ResourceOperation resourceOperation);
}