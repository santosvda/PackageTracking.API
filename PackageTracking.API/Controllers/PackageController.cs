using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Application.Packagess.Commands.CreatePackage;
using PackageTracking.Application.Receivers.Commands.DeletePackage;
using PackageTracking.Application.Receivers.Commands.UpdatePackage;
using PackageTracking.Application.Receivers.Queries.GetAllPackagesForReceiver;
using PackageTracking.Application.Receivers.Queries.GetAllPackagesForReceiverById;

namespace PackageTracking.API.Controllers;
[ApiController]
[Route("api/receiver/{receiverId}/[controller]")]
[Authorize]
public class PackageController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] int receiverId, [FromBody] CreatePackageCommand command)
    {
        command.ReceiverId = receiverId;
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackageDto>>> Get([FromRoute] int receiverId)
    {
        var packages = await mediator.Send(new GetAllPackagesForReceiverQuery { ReceiverId = receiverId });

        return Ok(packages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<PackageDto>>> GetById([FromRoute] int receiverId, [FromRoute] int id)
    {
        var packages = await mediator.Send(new GetAllPackagesForReceiverByIdQuery(receiverId, id));

        return Ok(packages);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int receiverId, [FromRoute] int id, [FromBody] UpdatePackageCommand command)
    {
        command.ReceiverId = receiverId;
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int receiverId, [FromRoute] int id)
    {
        await mediator.Send(new DeletePackageCommand(receiverId, id));

        return NoContent();
    }
}
