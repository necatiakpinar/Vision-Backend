using Microsoft.EntityFrameworkCore;
using Vision.Application.Repositories.OwnedFiles;
using Vision.Domain.Entities;
using Vision.Infrastructure.DBContexts;

namespace Vision.Infrastructure.Repositories.OwnedFiles;

public class OwnedFilesReadRepository : ReadRepository<OwnedFilesModel>, IOwnedFilesReadRepository
{
    public OwnedFilesReadRepository(VisionDbContext context) : base(context)
    {
    }
    
    public async Task<OwnedFilesModel?> GetOwnedFilesByOwnerId(string ownerId)
    {
        return await context.OwnedFiles
            .Include(x => x.Files)
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
    }
}