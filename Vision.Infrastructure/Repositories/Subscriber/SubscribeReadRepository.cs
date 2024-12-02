using Vision.Application.Repositories.Subscriber;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Subscriber;

public class SubscribeReadRepository : ReadRepository<FollowerModel>, ISubscriberReadRepository
{
    public SubscribeReadRepository(VisionDbContext context) : base(context)
    {
    }
}