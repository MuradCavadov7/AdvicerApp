using AdvicerApp.BL.ExternalServices.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AdvicerApp.BL.ExternalServices.Implements;

public class RedisService(IDistributedCache _redis) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        string? data = await _redis.GetStringAsync(key);
        if (data == null) return default(T);
        return JsonSerializer.Deserialize<T>(data);

    }

    public async Task RemoveAsync(string key)
    {
        await _redis.RemoveAsync(key);
    }

    public async Task SetAsync<T>(string key, T data, int minute = 1)
    {
        string json = JsonSerializer.Serialize(data);
        DistributedCacheEntryOptions time = new DistributedCacheEntryOptions();
        time.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minute);
        await _redis.SetStringAsync(key, json, time);

    }
}
