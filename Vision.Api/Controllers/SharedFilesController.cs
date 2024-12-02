using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.SharedFiles.Command.Add;
using Vision.Application.SharedFiles.Command.AddMultiple;
using Vision.Application.SharedFiles.Command.Update;
using Vision.Application.SharedFiles.Query.Get;

namespace Vision.Api.Controllers;

[Route("sharedfiles")]
[AllowAnonymous]
public class SharedFilesController : ApiController
{
    private readonly IMediator _mediator;

    public SharedFilesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetSharedFilesInfo([FromBody] GetSharedFileInfoQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddSharedFileInfo([FromBody] AddSharedFilesInfoCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> AddMultipleSharedFileInfo([FromBody] AddMultipleSharedFilesInfoCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    
    
    [HttpPatch("[action]")]
    public async Task<IActionResult> UpdateSharedFileInfo([FromBody] UpdateSharedFileInfoCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}

