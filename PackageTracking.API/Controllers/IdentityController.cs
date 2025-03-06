using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackageTracking.Application.Users.Commands.AssignUserRole;
using PackageTracking.Application.Users.Commands.UnassignUserRole;
using PackageTracking.Application.Users.Commands.UpdateUserDetails;
using PackageTracking.Domain.Constants;

namespace PackageTracking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole([FromBody] UnassignUserRoleCommand command)
    {
        await mediator.Send(command);

        return NoContent();
    }
}
