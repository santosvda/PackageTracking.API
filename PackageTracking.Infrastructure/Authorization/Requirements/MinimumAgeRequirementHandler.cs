using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Users;
using PackageTracking.Domain.Constants;

namespace PackageTracking.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger
    , IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User: {Email}, date of birth {Date} - Handling MinimumAgeRequirement",
            currentUser!.Email, currentUser.DateofBirth);

        if (currentUser.DateofBirth == null)
        {
            logger.LogInformation("Authorization failed: Date of birth is not set");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateofBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;
    }
}
