using Microsoft.AspNetCore.Authorization;

namespace PackageTracking.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirement(int minimunAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimunAge;
}
