using Vision.Application.Repositories.ProfileImage;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.ProfileImage;

public class ProfileImageReadRepository : ReadRepository<ImageModel>, IProfileImageReadRepository
{
    public ProfileImageReadRepository(VisionDbContext context) : base(context)
    {
    }
}