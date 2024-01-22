using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class ServiceDefinitionOptions
    {
        public string AppName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string NotifyBotUrl { get; set; }
        public string NotifyApiUrl { get; set; }
        public string RedirectUrl { get; set; }
        public bool EnableLogger { get; set; }
        //Default Value
        public ServiceDefinitionOptions()
        {
            NotifyBotUrl="https://notify-bot.line.me/oauth";
            NotifyApiUrl="https://notify-api.line.me/api";
            EnableLogger=false;
        }
    }
}
