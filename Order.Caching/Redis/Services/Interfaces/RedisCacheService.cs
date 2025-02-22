﻿namespace Order.Caching.Redis.Services.Interfaces
{
    public interface IRedisCacheService
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        Task<bool> RemoveAsync(string key);
    }
}
