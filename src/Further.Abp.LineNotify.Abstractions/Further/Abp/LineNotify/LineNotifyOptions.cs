using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class LineNotifyOptions 
    {
        public string NotifyBotUrl { get; set; } = "https://notify-bot.line.me/oauth";
        public string NotifyApiUrl { get; set; } = "https://notify-api.line.me/api";
        public Dictionary<string, ServiceDefinitionConfigurator> Configurators { get; set; } = new();
    }

    public class ServiceDefinitionConfigurator
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string RedirectUrl { get; set; } = null!;
    }
}
