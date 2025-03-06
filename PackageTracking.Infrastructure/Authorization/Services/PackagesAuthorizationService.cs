using Microsoft.Extensions.Logging;
using PackageTracking.Application.Users;
using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Interfaces;

namespace PackageTracking.Infrastructure.Authorization.Services;

public class PackagesAuthorizationService(ILogger<PackagesAuthorizationService> logger
    , IUserContext userContext
    ) : IPackagesAuthorizationService
{
    public bool Authorize(Package package, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorize user {User} to {Operation} for package {Package}"
            , user!.Email
            , resourceOperation
            , package.Id);

        if (new[] { ResourceOperation.Create, ResourceOperation.Read }.Contains(resourceOperation))
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Delete operation - successful authorization for admin");
            return true;
        }

        if (new[] { ResourceOperation.Delete, ResourceOperation.Update }.Contains(resourceOperation) && user.Id == package.OwnerId)
        {
            logger.LogInformation("Package Owner Delete/Update operation - successful authorization");
            return true;
        }

        return false;
    }
}
