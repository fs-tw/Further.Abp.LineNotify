using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.MultiTenancy;

namespace Further.Abp.LineNotify
{
    public static class DistributedCacheExtensions
    {
        public static async Task KeyPersistAsync(this IDistributedCache cache, string key)
        {
            var redisDatabaseProperty = typeof(AbpRedisCache).GetProperty("RedisDatabase", BindingFlags.Instance | BindingFlags.NonPublic);

            var redisDatabase = (StackExchange.Redis.IDatabase)redisDatabaseProperty.GetValue(cache);

            await redisDatabase.KeyPersistAsync(key);
        }

        public static string GetFullKey<TCacheItem>(this IDistributedCacheKeyNormalizer keyNormalizer, string key)
        {
            var cacheName = CacheNameAttribute.GetCacheName(typeof(TCacheItem));

            var ignoreMultiTenancy = typeof(TCacheItem).IsDefined(typeof(IgnoreMultiTenancyAttribute), true);

            return keyNormalizer.NormalizeKey(
                new DistributedCacheKeyNormalizeArgs(
                    key.ToString()!,
                    cacheName,
                    ignoreMultiTenancy
                    )
                );
        }
    }
}
