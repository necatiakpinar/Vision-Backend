using Vision.Domain.Entities;

namespace Vision.Application.Repositories.OwnedFiles;

public interface IOwnedFilesReadRepository : IReadRepository<OwnedFilesModel>
{
    public Task<OwnedFilesModel?> GetOwnedFilesByOwnerId(string ownerId);
}