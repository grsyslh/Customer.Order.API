using StackExchange.Redis;
using System.Text.Json;
using Order.Caching.Redis.Services.Interfaces;

namespace Order.Caching.Redis.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _redisDb;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _redisDb.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _redisDb.StringSetAsync(key, serializedValue, expiry);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _redisDb.KeyDeleteAsync(key);
        }
    }
}
