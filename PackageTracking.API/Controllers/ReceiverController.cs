using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackageTracking.Application.Receivers.Commands.CreateReceiver;
using PackageTracking.Application.Receivers.Commands.DeleteReceiver;
using PackageTracking.Application.Receivers.Commands.UpdateReceiver;
using PackageTracking.Application.Receivers.Dtos;
using PackageTracking.Application.Receivers.Queries.GetAllReceivers;
using PackageTracking.Application.Receivers.Queries.GetReceiverById;

namespace PackageTracking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReceiverController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReceiverDto>>> Get()
    {
        var receivers = await mediator.Send(new GetAllReceiversQuery());

        return Ok(receivers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReceiverDto>> GetById([FromRoute] int id)
    {
        var receivers = await mediator.Send(new GetReceiverByIdQuery() { Id = id } );

        return Ok(receivers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReceiverCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReceiverCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteReceiverCommand(id));

        return NoContent();
    }
}
