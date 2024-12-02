using Vision.Application.Repositories.UserMedia;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.UserMedia;

public class UserMediaWriteRepository : WriteRepository<FileModel>, IUserMediaWriteRepository
{
    public UserMediaWriteRepository(VisionDbContext context) : base(context)
    {
    }
}
