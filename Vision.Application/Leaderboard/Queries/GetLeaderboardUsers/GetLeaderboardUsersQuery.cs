using MediatR;
using Vision.Application.Authentication.Common;

namespace Vision.Application.Leaderboard.Queries.GetLeaderboardUsers;

public class GetLeaderboardUsersQuery : IRequest<GenericResponse<GetLeaderboardUsersResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}