using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.OwnedFiles.Query.Get;

namespace Vision.Api.Controllers;

[ApiController]
[Route("ownedfiles")]
[AllowAnonymous]
public class OwnedFilesController : ApiController
{
    private IMediator _mediator;

    public OwnedFilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetOwnedFilesInfo([FromBody] GetOwnedFilesInfoCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}