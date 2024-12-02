using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Vision.Application.Services;
using Vision.Domain.Configurations;
using Vision.Domain.Identity;

namespace Vision.Infrastructure.Services;

public class MongoDbService : IMongoDbService
{
    private readonly IMongoDatabase _database;
    public IMongoDatabase  Database=> _database;

    public MongoDbService(IOptions<MongoDbConfig> mongoDbConfig)
    {
        var config = mongoDbConfig.Value;
        var settings = MongoClientSettings.FromConnectionString(config.ConnectionString);
        var client = new MongoClient(settings);
        _database = client.GetDatabase(config.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName = null)
    {
        return _database.GetCollection<T>(collectionName ?? typeof(T).Name.ToLowerInvariant());
    }


}