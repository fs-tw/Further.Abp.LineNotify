using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class LineNotifyOptions
    {
        public const string DefaultConfiguratorName = "Default";
        public static string HttpClientName(string configuratorName) => $"LineNotify_{configuratorName}";
        public string NotifyBotUrl { get; set; } = "https://notify-bot.line.me/oauth";
        public string NotifyApiUrl { get; set; } = "https://notify-api.line.me/api";
        public Dictionary<string, Configurator> Configurators { get; set; } = new();
        public class Configurator
        {
            public string ClientId { get; set; } = null!;
            public string ClientSecret { get; set; } = null!;
            public string RedirectUrl { get; set; } = null!;
        }
    }


}
