﻿using Application.Services.Cache.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Application.Services.Cache.Implementations;

public class ResponseCacheService: IResponseCacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
    {
        _distributedCache = distributedCache;
        _connectionMultiplexer = connectionMultiplexer;
    }
    
    public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeout)
    {
        var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
        {
          ContractResolver  = new CamelCasePropertyNamesContractResolver()
        });
        await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = timeout
        });
    }

    public async Task RemoveCacheResponseAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern))
        {
            throw new ArgumentException("Value can not be null or whitespace");
        }

        await foreach (var key in GetKeyAsync(pattern + "*"))
        {
            await _distributedCache.RemoveAsync(key);
        }
    }

    private async IAsyncEnumerable<string> GetKeyAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern))
        {
            throw new ArgumentException("Value can not be null or whitespace");
        }

        foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
        {
            var server = _connectionMultiplexer.GetServer(endPoint);
            foreach (var key in server.Keys(pattern: pattern))
            {
                yield return key.ToString();
            }
        }
    }

    public async Task<string> GetCacheResponseAsync(string cacheKey)
    {
        var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(cacheResponse) ? null! : cacheResponse;
    }
}