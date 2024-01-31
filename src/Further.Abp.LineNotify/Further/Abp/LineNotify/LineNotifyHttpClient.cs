using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Json;

namespace Further.Abp.LineNotify
{
    public class LineNotifyHttpClient : Volo.Abp.DependencyInjection.ITransientDependency, ILineNotifyHttpClient
    {
        //想擴充logger功能可參考
        //https://learn.microsoft.com/en-us/dotnet/api/system.net.http.delegatinghandler?view=net-8.0
        //實作DelegatingHandler

        private readonly LineNotifyOptions options;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IJsonSerializer jsonSerializer;

        public LineNotifyHttpClient(
            IOptions<LineNotifyOptions> options,
            IHttpClientFactory httpClientFactory,
            IJsonSerializer jsonSerializer)

        {
            this.options = options.Value;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializer = jsonSerializer;
        }

        public async Task<string> AuthorizeUrlAsync(string state)
        {

            var defaultConfigurator = options.Configurators["Default"];
            var url = $"{options.NotifyBotUrl}/authorize?response_type=code&client_id={defaultConfigurator.ClientId}&redirect_uri={defaultConfigurator.RedirectUrl}&scope=notify&state={state}";
            return url;
        }

        public async Task NotifyAsync(string accessToken, string message)
        {
            var client = httpClientFactory.CreateClient("LineNotify_Default");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{options.NotifyApiUrl}/notify?message={message}");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.SendAsync(request);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                jsonSerializer.Deserialize<NotifyResult>(jsonString);
            }
        }

        public async Task<string> TokenAsync(string code)
        {
            var defaultConfigurator = options.Configurators["Default"];

            var client = httpClientFactory.CreateClient("LineNotify_Default");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{options.NotifyBotUrl}/token");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", defaultConfigurator.ClientId),
                new KeyValuePair<string, string>("client_secret", defaultConfigurator.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", defaultConfigurator.RedirectUrl),
            });

            request.Content = formContent;

            var response = await client.SendAsync(request);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return jsonSerializer.Deserialize<TokenResult>(jsonString).access_token;
            }

            throw new NotImplementedException();
        }
    }
}
