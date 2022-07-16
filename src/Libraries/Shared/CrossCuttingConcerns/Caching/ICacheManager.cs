namespace Shared.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    T GetCache<T>(string key);
    object GetCache(string key);
    void AddCache(string key, object data, int duration);
    void RemoveCache(string key);
    void RemoveCacheByPattern(string pattern);
    bool IsAddedCache(string key);
}