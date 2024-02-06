using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Further.Abp.LineNotify
{
    public class AccessTokenCacheItem
    {
        public string AccessToken { get; set; } = null!;
    }

    public interface IAccessTokenProvider
    {
        Task<AccessTokenCacheItem?> GetAccessTokenAsync(string configuratorName, string subject);

        Task SetAccessTokenAsync(string configuratorName, string subject, string token);

        Task RemoveAccessTokenAsync(string configuratorName, string subject);
    }

}
