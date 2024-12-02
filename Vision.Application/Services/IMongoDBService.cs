using MongoDB.Driver;

namespace Vision.Application.Services;

public interface IMongoDbService
{
    public IMongoDatabase Database { get; }
    IMongoCollection<T> GetCollection<T>(string collectionName = null);
    
}