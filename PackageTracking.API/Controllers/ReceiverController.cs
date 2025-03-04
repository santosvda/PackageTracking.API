using MediatR;
using Microsoft.AspNetCore.Mvc;
using PackageTracking.Application.Receivers.Commands.CreateReceiver;
using PackageTracking.Application.Receivers.Commands.DeleteReceiver;
using PackageTracking.Application.Receivers.Commands.UpdateReceiver;
using PackageTracking.Application.Receivers.Queries.GetAllReceivers;
using PackageTracking.Application.Receivers.Queries.GetReceiverById;

namespace PackageTracking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReceiverController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var receivers = await mediator.Send(new GetAllReceiversQuery());

        return Ok(receivers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var receivers = await mediator.Send(new GetReceiverByIdQuery() { Id = id } );

        if (receivers is null)
            return NotFound();

        return Ok(receivers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReceiverCommand command)
    {
        var id = await mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReceiverCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);

        if (isUpdated)
            return NoContent();

        return NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeleteReceiverCommand(id));

        if (isDeleted)
            return NoContent();

        return NotFound();
    }
}
