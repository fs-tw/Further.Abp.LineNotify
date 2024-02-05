using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public class LineNotifyConsts
    {
        public const string DefaultConfiguratorName = "Default";
        public const string DefaultGroupName = "Default";
        public static string HttpClientName(string configuratorName) => $"LineNotify_{configuratorName}";
        public static string AccessTokenCacheName(string configuratorName, string groupName) => $"LineNotify_{configuratorName}-{groupName}_AccessToken";
    }
}
