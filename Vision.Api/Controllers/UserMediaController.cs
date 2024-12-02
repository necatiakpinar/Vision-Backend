using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.Authentication.Common;
using Vision.Application.UserMedia.Commands.Upload;
using Vision.Application.UserMedia.Queries.Get;

namespace Vision.Api.Controllers;

[Route("usermedia")]
[AllowAnonymous]
public class UserMediaController : ApiController
{
    private readonly IMediator _mediator;

    public UserMediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(UploadUserMediaCommand command)
    {
        if (command.File == null)
        {
            return BadRequest("No file uploaded.");
        }

        GenericResponse<UploadUserMediaResponse> response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetFile(GetUserMediaQuery ınfoQuery)
    {
        GenericResponse<GetUserMediaResponse> response = await _mediator.Send(ınfoQuery);
        return Ok(response);
    }
    
}