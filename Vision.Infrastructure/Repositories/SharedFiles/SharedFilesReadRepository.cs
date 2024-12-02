using Vision.Application.Repositories.SharedFiles;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.SharedFiles;

public class SharedFilesReadRepository : ReadRepository<SharedFilesModel>, ISharedFilesReadRepository
{
    public SharedFilesReadRepository(VisionDbContext context) : base(context)
    {
    }
}