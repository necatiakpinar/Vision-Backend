using Vision.Application.Repositories.ProfileImage;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.ProfileImage;

public class ProfileImageWriteRepository :  WriteRepository<ImageModel>, IProfileImageWriteRepository
{
    public ProfileImageWriteRepository(VisionDbContext context) : base(context)
    {
    }
}