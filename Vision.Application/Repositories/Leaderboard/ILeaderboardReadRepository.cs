namespace Vision.Application.Repositories.Leaderboard;

public interface ILeaderboardReadRepository : IReadRepository<Domain.Entities.Leaderboard>
{
    Task<Domain.Entities.Leaderboard?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}