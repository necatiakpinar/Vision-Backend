using Vision.Application.Repositories.Subscriber;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.Subscriber;

public class SubscribeWriteRepository : WriteRepository<FollowerModel>, ISubscriberWriteRepository
{
    public SubscribeWriteRepository(VisionDbContext context) : base(context)
    {
    }
}