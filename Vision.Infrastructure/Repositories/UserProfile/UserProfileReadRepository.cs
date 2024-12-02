using Vision.Application.Repositories.UserProfile;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.UserProfile;

public class UserProfileReadRepository : ReadRepository<Domain.Entities.UserProfile>, IUserProfileReadRepository
{
    public UserProfileReadRepository(VisionDbContext context) : base(context)
    {
    }
}