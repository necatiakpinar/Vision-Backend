using Vision.Application.Repositories.SharedFiles;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.SharedFiles;

public class SharedFilesWriteRepository : WriteRepository<SharedFilesModel> , ISharedFilesWriteRepository
{
    public SharedFilesWriteRepository(VisionDbContext context) : base(context)
    {
    }
}