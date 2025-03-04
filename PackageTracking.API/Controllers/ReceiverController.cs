using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PackageTracking.Application.Receivers;
using PackageTracking.Application.Receivers.Dtos;

namespace PackageTracking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReceiverController(IReceiverService receiverService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var receivers = await receiverService.GetReceivers();

        return Ok(receivers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var receivers = await receiverService.GetReceiverById(id);

        if(receivers is null)
        {
            return NotFound();
        }

        return Ok(receivers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReceiverDto command)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await receiverService.Insert(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}
