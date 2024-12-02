using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.UserMedia;

public class UserMediaReadRepository: ReadRepository<FileModel>, IUserMediaReadRepository
{
    public UserMediaReadRepository(VisionDbContext context) : base(context)
    {
    }
}
