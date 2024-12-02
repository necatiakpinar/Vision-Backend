using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;

namespace Vision.Application.Leaderboard.Queries.GetLeaderboardUsers;

public class GetLeaderboardUsersResponse : IResponse
{
    public IList<Domain.Entities.Leaderboard> LeaderboardUsers { get; set; }
    public WarningResult? WarningResult { get; set; }
}