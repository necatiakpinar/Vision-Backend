using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Application.Authentication.Common;
using Vision.Application.Leaderboard.Commands.AddOrUpdateUser;
using Vision.Application.Leaderboard.Queries;
using Vision.Application.Leaderboard.Queries.GetLeaderboardUsers;
using Vision.Contracts.Leaderboard;

namespace Vision.Api.Controllers;

[Route("leaderboard")]
[AllowAnonymous]
public class LeaderboardController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public LeaderboardController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddOrUpdateUser(AddUserToLeaderboardRequest request)
    {
        var command = _mapper.Map<AddOrUpdateUserToLeaderboardCommand>(request);
        GenericResponse<AddOrUpdateUserToLeaderboardResponse> response = await _mediator.Send(command);

        return Ok(_mapper.Map<GenericResponse<AddOrUpdateUserToLeaderboardResponse>>(response));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetLeaderboardUsers([FromBody] GetLeaderboardUsersRequest request)
    {
        var query = _mapper.Map<GetLeaderboardUsersQuery>(request);
        GenericResponse<GetLeaderboardUsersResponse> response = await _mediator.Send(query);

        return Ok(_mapper.Map<GenericResponse<GetLeaderboardUsersResponse>>(response));
    }
    
}