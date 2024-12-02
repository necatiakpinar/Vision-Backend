using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.Mentor.Command.AddMentorFollower;
using Vision.Application.Mentor.Command.CreateMentor;
using Vision.Application.Mentor.Command.DeleteMentor;
using Vision.Application.Mentor.Command.DeleteMentorFollower;

namespace Vision.Api.Controllers;

[Route("mentor")]
[AllowAnonymous]
public class MentorController : ApiController
{
    private readonly IMediator _mediator;

    public MentorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateMentor([FromBody] CreateMentorCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteMentor([FromBody] DeleteMentorCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
     [HttpPatch("[action]")]
     public async Task<IActionResult> AddMentorFollower([FromBody] AddMentorFollowerCommand command)
     {
         var response = await _mediator.Send(command);
         return Ok(response);
     }

     [HttpPatch("[action]")]
     public async Task<IActionResult> DeleteMentorFollower([FromBody] DeleteMentorFollowerCommand command)
     {
         var response = await _mediator.Send(command);
         return Ok(response);
     }

}