using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace Further.Abp.LineNotify
{
    public class DefaultAccessTokenProvider : IAccessTokenProvider, Volo.Abp.DependencyInjection.ITransientDependency
    {
        private readonly IDistributedCache<AccessTokenCacheItem> cache;

        public DefaultAccessTokenProvider(IDistributedCache<AccessTokenCacheItem> cache)
        {
            this.cache = cache;
        }

        public async Task<AccessTokenCacheItem?> GetAccessTokenAsync(string configuratorName, string subject)
        {

            var result = await cache.GetAsync(LineNotifyConsts.AccessTokenCacheName(configuratorName, subject));

            return result;
        }

        public async Task SetAccessTokenAsync(string configuratorName, string subject, string value)
        {
            await cache.SetAsync(LineNotifyConsts.AccessTokenCacheName(configuratorName, subject), new AccessTokenCacheItem { AccessToken = value });
        }

        public async Task RemoveAccessTokenAsync(string configuratorName, string subject)
        {
            await cache.RemoveAsync(LineNotifyConsts.AccessTokenCacheName(configuratorName, subject));
        }


    }
}
