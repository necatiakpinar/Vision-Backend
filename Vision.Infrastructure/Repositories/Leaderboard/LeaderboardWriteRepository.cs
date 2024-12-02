using Vision.Application.Repositories.Leaderboard;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Leaderboard;

public class LeaderboardWriteRepository : WriteRepository<Domain.Entities.Leaderboard>, ILeaderboardWriteRepository
{
    public LeaderboardWriteRepository(VisionDbContext context) : base(context)
    {
    }
}