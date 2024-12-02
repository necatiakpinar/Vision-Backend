namespace Vision.Contracts.Leaderboard;

public record class AddUserToLeaderboardRequest(string UserId, int Score);