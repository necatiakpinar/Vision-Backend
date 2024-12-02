namespace Vision.Application.Services;

public interface IRedisService
{
    void SetValue(string key, string value, TimeSpan? expiry = null);
    string GetValue(string key);
    bool KeyExists(string key);
    void DeleteKey(string key);
    void IncrementValue(string key);
    void DecrementValue(string key);
}