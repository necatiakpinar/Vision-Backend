using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace Vision.Application.Services;

public interface IGridFsService
{
    public Task<ObjectId> UploadImageAsync(Stream fileStream, string fileName);
    public Task<byte[]> DownloadImageAsync(ObjectId id);  
    public Task<List<GridFSFileInfo<ObjectId>>> GetAllFilesAsync();



}