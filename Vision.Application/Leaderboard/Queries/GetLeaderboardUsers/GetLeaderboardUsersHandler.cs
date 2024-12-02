using MediatR;
using Microsoft.EntityFrameworkCore;
using Vision.Application.Authentication.Common;
using Vision.Application.Common;
using Vision.Application.Repositories.Leaderboard;
using Vision.Domain.Entities;

namespace Vision.Application.Leaderboard.Queries.GetLeaderboardUsers;

public class GetLeaderboardUsersHandler : IRequestHandler<GetLeaderboardUsersQuery, GenericResponse<GetLeaderboardUsersResponse>>
{
    private readonly ILeaderboardReadRepository _leaderboardReadRepository;
    private readonly int _minPageSize = 1;

    public GetLeaderboardUsersHandler(ILeaderboardReadRepository leaderboardReadRepository)
    {
        _leaderboardReadRepository = leaderboardReadRepository;
    }

    public async Task<GenericResponse<GetLeaderboardUsersResponse>> Handle(GetLeaderboardUsersQuery request, CancellationToken cancellationToken)
    {
        if (request.PageNumber < _minPageSize || request.PageSize < _minPageSize)
        {
            return new GenericResponse<GetLeaderboardUsersResponse>()
            {
                WarningResult = new WarningResult()
                {
                    Title = "Invalid Page Number or Page Size",
                    Message = "Page number or size must be greater than 0"
                }
            };
        }

        var leaderboardUsers = await _leaderboardReadRepository.Table
            .OrderByDescending(x => x.Score)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GenericResponse<GetLeaderboardUsersResponse>
        {
            Data = new GetLeaderboardUsersResponse()
            {
                LeaderboardUsers = leaderboardUsers
            }
        };
    }
}