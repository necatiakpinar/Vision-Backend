using StackExchange.Redis;
using Vision.Application.Services;

namespace Vision.Infrastructure.Services;

public class RedisService : IRedisService
{
    private readonly IDatabase _db;

    public RedisService(string connectionString)
    {
        var redis = ConnectionMultiplexer.Connect(connectionString);
        _db = redis.GetDatabase();
    }

    public void SetValue(string key, string value, TimeSpan? expiry = null)
    {
        _db.StringSet(key, value, expiry);
    }

    public string GetValue(string key)
    {
        return _db.StringGet(key);
    }

    public bool KeyExists(string key)
    {
        return _db.KeyExists(key);
    }

    public void DeleteKey(string key)
    {
        _db.KeyDelete(key);
    }

    public void IncrementValue(string key)
    {
        _db.StringIncrement(key);
    }

    public void DecrementValue(string key)
    {
        _db.StringDecrement(key);
    }
}