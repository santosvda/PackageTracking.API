using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;
using System.Security.Claims;

namespace PackageTracking.Infrastructure.Authorization;

public class PackageTrackingUserClaimsPrincipalFactory(UserManager<User> userManager
    , RoleManager<IdentityRole> roleManager
    , IOptions<IdentityOptions> optionsAccessor
    ) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    public async override Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if(user.Nationality != null)
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

        if (user.DateofBirth != null)
            id.AddClaim(new Claim(AppClaimTypes.DateofBirth, user.DateofBirth.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(id);
    }
}
