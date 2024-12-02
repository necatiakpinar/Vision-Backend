using Vision.Application.Authentication.Common.Interfaces;
using Vision.Application.Common;

namespace Vision.Application.Leaderboard.Commands.AddOrUpdateUser;

public class AddOrUpdateUserToLeaderboardResponse : IResponse
{
    public bool Succeeded { get; set; }
    public WarningResult? WarningResult { get; set; }
}