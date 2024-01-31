using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Further.Abp.LineNotify
{

    public class CurrentClient : ICurrentClient, Volo.Abp.DependencyInjection.ISingletonDependency
    {
        public string ClientKey { get; set; } = "Default";

        public IDisposable SetCurrent(string clientKey)
        {
            var key = ClientKey;
            ClientKey = clientKey;
            return new DisposeAction(() =>
            {
                this.ClientKey = key;
            });
        }
    }
}
