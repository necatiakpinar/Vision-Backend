using Vision.Application.Repositories.User;
using Vision.Application.Repositories.UserProfile;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.UserProfile;

public class UserProfileWriteRepository : WriteRepository<Domain.Entities.UserProfile>, IUserProfileWriteRepository
{
    public UserProfileWriteRepository(VisionDbContext context) : base(context)
    {
    }
}