using Vision.Application.Repositories.OwnedFiles;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.OwnedFiles;

public class OwnedFilesWriteRepository : WriteRepository<OwnedFilesModel>, IOwnedFilesWriteRepository
{
    public OwnedFilesWriteRepository(VisionDbContext context) : base(context)
    {
    }
    
}