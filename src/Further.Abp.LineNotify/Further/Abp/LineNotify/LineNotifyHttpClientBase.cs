using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Further.Abp.LineNotify
{
    public abstract class LineNotifyHttpClientBase
    {
        protected readonly ServiceDefinitionOptions options;

        private readonly Dictionary<string, Lazy<HttpClient>> lazyHttpClients = new();

        public LineNotifyHttpClientBase(
            IOptions<ServiceDefinitionOptions> options,
            IHttpClientFactory httpClientFactory)
        {
            this.options = options.Value;

            lazyHttpClients[this.options.AppName] =
                    new Lazy<HttpClient>(() =>
                    {
                        var http = httpClientFactory.CreateClient(this.options.AppName);
                        http.Timeout = TimeSpan.FromMinutes(30);
                        return http;
                    }, true);
        }

        protected virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, string resource, string? accessToken = null)
        {
            var httpClient = lazyHttpClients[options.AppName].Value;

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return await httpClient.SendAsync(request);
        }

        protected virtual string TrimContent(string content)
        {
            return content?.Trim(new char[] { '\uFEFF', '\u200B' });
        }
    }
}
