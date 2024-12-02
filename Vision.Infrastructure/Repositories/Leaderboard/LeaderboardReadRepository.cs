using Microsoft.EntityFrameworkCore;
using Vision.Application.Repositories.Leaderboard;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Leaderboard
{
    public class LeaderboardReadRepository : ReadRepository<Domain.Entities.Leaderboard>, ILeaderboardReadRepository
    {
        public LeaderboardReadRepository(VisionDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.Leaderboard?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await context.Leaderboards
                .FirstOrDefaultAsync(lb => lb.UserId == userId, cancellationToken);
        }
    }
}