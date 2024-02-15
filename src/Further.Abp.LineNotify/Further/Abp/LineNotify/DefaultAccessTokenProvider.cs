using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Further.Abp.LineNotify
{
    public class DefaultAccessTokenProvider : IAccessTokenProvider, Volo.Abp.DependencyInjection.ITransientDependency
    {
        private readonly IDistributedCache<AccessTokenCacheItem> cache;
        private readonly IDistributedCache distributedCache;
        private readonly IDistributedCacheKeyNormalizer keyNormalizer;

        public DefaultAccessTokenProvider(
            IDistributedCache<AccessTokenCacheItem> cache,
            IDistributedCache distributedCache,
            IDistributedCacheKeyNormalizer keyNormalizer)
        {
            this.cache = cache;
            this.distributedCache = distributedCache;
            this.keyNormalizer = keyNormalizer;
        }

        public async Task<AccessTokenCacheItem?> GetAccessTokenAsync(string configuratorName, string subject)
        {

            var result = await cache.GetAsync(LineNotifyConsts.AccessTokenCacheName(configuratorName, subject));

            return result;
        }

        public async Task SetAccessTokenAsync(string configuratorName, string subject, string value)
        {
            var key = LineNotifyConsts.AccessTokenCacheName(configuratorName, subject);
            var fullKey = keyNormalizer.GetFullKey<AccessTokenCacheItem>(key);

            await cache.SetAsync(key, new AccessTokenCacheItem { AccessToken = value });

            await distributedCache.KeyPersistAsync(fullKey);
        }

        public async Task RemoveAccessTokenAsync(string configuratorName, string subject)
        {
            await cache.RemoveAsync(LineNotifyConsts.AccessTokenCacheName(configuratorName, subject));
        }


    }
}
