using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.Authentication.Common;
using Vision.Application.Image.ProfileImage.Commands.Upload;
using Vision.Application.Image.ProfileImage.Queries.Get;

namespace Vision.Api.Controllers;

[Route("/images/profileimage")]
[ApiController]
[AllowAnonymous]
public class ProfileImageController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProfileImageController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadImage(UploadProfileImageCommand command)
    {
        if (command.File == null)
        {
            return BadRequest("No file uploaded.");
        }

        GenericResponse<UploadProfileImageResponse> response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetProfileImage([FromBody ] GetProfileImageQuery query)
    {
        GenericResponse<GetProfileImageResponse> response = await _mediator.Send(query);
        return Ok(response);
    }
}