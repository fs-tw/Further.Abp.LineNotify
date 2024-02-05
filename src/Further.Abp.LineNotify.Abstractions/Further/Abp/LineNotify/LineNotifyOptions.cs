using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Further.Abp.LineNotify
{
    public class LineNotifyOptions
    {
        public const string DefaultConfiguratorName = "Default";
        public static string HttpClientName(string configuratorName) => $"LineNotify_{configuratorName}";
        public string NotifyBotUrl { get; set; } = "https://notify-bot.line.me/oauth";
        public string NotifyApiUrl { get; set; } = "https://notify-api.line.me/api";
        public Dictionary<string, Configurator> Configurators { get; set; } = new();
        public class Configurator : Dictionary<string, string?>
        {
            public string ClientId
            {
                get => this.GetOrDefault(nameof(ClientId))!;
                set => this[nameof(ClientId)] = value;
            }
            public string ClientSecret
            {
                get => this.GetOrDefault(nameof(ClientSecret))!;
                set => this[nameof(ClientSecret)] = value;
            }
            public string RedirectUrl
            {
                get => this.GetOrDefault(nameof(RedirectUrl))!;
                set => this[nameof(RedirectUrl)] = value;
            }
        }
    }

    public static class ConfiguratorExtensions
    {
        public const string AccessTokenName = "AccessToken";
        [CanBeNull]
        public static string GetAccessToken([NotNull] this LineNotifyOptions.Configurator configuration)
        {
            Check.NotNullOrEmpty(configuration, nameof(configuration));

            var value = configuration.GetOrDefault(AccessTokenName);
            if (value == null)
            {
                return null;
            }

            return value;
        }

        public static LineNotifyOptions.Configurator SetAccessToken([NotNull] this LineNotifyOptions.Configurator configuration, [CanBeNull] string value)
        {
            if (value == null)
            {
                configuration.Remove(AccessTokenName);
            }
            else
            {
                configuration[AccessTokenName] = value;
            }

            return configuration;
        }
    }

}
