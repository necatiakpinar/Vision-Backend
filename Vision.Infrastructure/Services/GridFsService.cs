using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Vision.Application.Services;

public class GridFsService : IGridFsService
{
    private readonly IMongoDatabase _database;
    private readonly GridFSBucket _gridFS;

    public GridFsService(IMongoDbService mongoDbService)
    {
        _database = mongoDbService.Database;
        _gridFS = new GridFSBucket(_database);
    }

    public async Task<ObjectId> UploadImageAsync(Stream fileStream, string fileName)
    {
        ObjectId fileId = await _gridFS.UploadFromStreamAsync(fileName, fileStream);
        return fileId;
    }

    public async Task<byte[]> DownloadImageAsync(ObjectId id)
    {
        try
        {
            return await _gridFS.DownloadAsBytesAsync(id);
        }
        catch (GridFSFileNotFoundException)
        {
            return null;
        }
    }

    public async Task<List<GridFSFileInfo<ObjectId>>> GetAllFilesAsync()
    {
        var filter = Builders<GridFSFileInfo<ObjectId>>.Filter.Empty;
        return await _gridFS.Find(filter).ToListAsync();
    }
}