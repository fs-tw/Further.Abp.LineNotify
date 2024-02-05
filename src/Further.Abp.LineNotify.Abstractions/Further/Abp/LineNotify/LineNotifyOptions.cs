using System;
using System.Collections.Generic;
using System.Text;

namespace Further.Abp.LineNotify
{
    public class LineNotifyOptions
    {
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
            public string ResultUrl
            {
                get => this.GetOrDefault(nameof(ResultUrl))!;
                set => this[nameof(ResultUrl)] = value;
            }
            public string RedirectUrl
            {
                get => this.GetOrDefault(nameof(RedirectUrl))!;
                set => this[nameof(RedirectUrl)] = value;
            }
        }
    }

}
