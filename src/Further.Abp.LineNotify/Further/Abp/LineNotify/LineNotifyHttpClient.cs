using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Json;

namespace Further.Abp.LineNotify
{
    public class LineNotifyHttpClient : Volo.Abp.DependencyInjection.ITransientDependency, ILineNotifyHttpClient
    {
        //想擴充logger功能可參考
        //https://learn.microsoft.com/en-us/dotnet/api/system.net.http.delegatinghandler?view=net-8.0
        //實作DelegatingHandler

        private const string NotifyBotUrl = "https://notify-bot.line.me/oauth";
        private const string NotifyApiUrl = "https://notify-api.line.me/api";
        private readonly LineNotifyOptions options;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IAccessTokenProvider accessTokenProvider;

        public LineNotifyHttpClient(
            IOptions<LineNotifyOptions> options,
            IHttpClientFactory httpClientFactory,
            IJsonSerializer jsonSerializer,
            IAccessTokenProvider accessTokenProvider)
        {
            this.options = options.Value;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = jsonSerializer;
            this.accessTokenProvider = accessTokenProvider;
        }

        public async Task<string> AuthorizeAsync(string resultUrl, string configuratorName = LineNotifyConsts.DefaultConfiguratorName, string groupName = LineNotifyConsts.DefaultGroupName)
        {

            var configurator = options.Configurators[configuratorName];
            var url = $"{NotifyBotUrl}/authorize?response_type=code&client_id={configurator.ClientId}&redirect_uri={configurator.RedirectUrl}&scope=notify&state={configuratorName}_{groupName}_{resultUrl ?? configurator.ResultUrl}";
            return url;
        }

        public async Task NotifyAsync(string message, string configuratorName = LineNotifyConsts.DefaultConfiguratorName, string groupName = LineNotifyConsts.DefaultGroupName)
        {
            var client = httpClientFactory.CreateClient(LineNotifyConsts.HttpClientName(configuratorName));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{NotifyApiUrl}/notify?message={message}");

            var token = (await accessTokenProvider.GetAccessTokenAsync(configuratorName, groupName))?.AccessToken;

            if (token == null)
            {
                throw new AbpException($"{configuratorName} of LineNotifyHttpClient has no token");
            }

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                jsonSerializer.Deserialize<NotifyResult>(jsonString);
            }
        }

        public async Task<string> TokenAsync(string code, string configuratorName = LineNotifyConsts.DefaultConfiguratorName)
        {
            var configurator = options.Configurators[configuratorName];

            var client = httpClientFactory.CreateClient(LineNotifyConsts.HttpClientName(configuratorName));

            var request = new HttpRequestMessage(HttpMethod.Post, $"{NotifyBotUrl}/token");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", configurator.ClientId),
                new KeyValuePair<string, string>("client_secret", configurator.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", configurator.RedirectUrl),
            });

            request.Content = formContent;

            var response = await client.SendAsync(request);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return jsonSerializer.Deserialize<TokenResult>(jsonString).access_token;
            }

            throw new ArgumentException();
        }
    }
}
