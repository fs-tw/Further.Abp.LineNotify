using HttpTracer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public class LoggerWraper<T> : HttpTracer.Logger.ILogger
    {
        private readonly ILogger<T> logger;

        public LoggerWraper(
            ILogger<T> logger)
        {
            this.logger=logger;
        }
        public void Log(string message)
        {
            this.logger.LogInformation(message);
        }
    }
    public class LineNotifyHttpClient2 : ILineNotifyHttpClient, Volo.Abp.DependencyInjection.ITransientDependency
    {
        private RestClient notifyBotClient;
        private RestClient notifyApiClient;
        private readonly ServiceDefinitionOptions serviceDefinitionOptions;

        public LineNotifyHttpClient2(
            IOptions<ServiceDefinitionOptions> serviceDefinitionOptions,
            ILogger<LineNotifyHttpClient> logger
            )
        {

            this.serviceDefinitionOptions = serviceDefinitionOptions.Value;

            this.notifyBotClient=new RestClient(createRestClientOptions(this.serviceDefinitionOptions.NotifyBotUrl));

            this.notifyApiClient=new RestClient(createRestClientOptions(this.serviceDefinitionOptions.NotifyApiUrl));

            RestClientOptions createRestClientOptions(string url)
            {
                var result = new RestClientOptions(url);

                if (this.serviceDefinitionOptions.EnableLogger)
                {
                    var loggerWraper = new LoggerWraper<LineNotifyHttpClient>(logger);
                    result.ConfigureMessageHandler = handler => new HttpTracerHandler(handler, loggerWraper, HttpMessageParts.All);
                }

                return result;
            }
        }
        protected RestRequest CreateRequest(string resource, string accessToken = null)
        {
            var request = new RestRequest(resource);

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.AddParameter("Authorization", $"Bearer {accessToken}", ParameterType.HttpHeader);
            }

            return request;
        }

        public Task<string> AuthorizeUrlAsync(string state)
        {
            var request = this.CreateRequest("authorize");

            //var state = $"name={name}&provider-name={providerName}&provider-key={providerKey}";

            request.AddQueryParameter("response_type", "code");

            request.AddQueryParameter("client_id", serviceDefinitionOptions.ClientId);

            request.AddQueryParameter("redirect_uri", serviceDefinitionOptions.RedirectUrl);

            request.AddQueryParameter("scope", "notify");

            request.AddQueryParameter("state", state);

            return Task.FromResult(notifyBotClient.BuildUri(request).ToString());
        }

        public async Task<GetTokenResult> TokenAsync(string code)
        {
            var request = this.CreateRequest("token");

            var requestObject = new GetToken()
            {
                client_id = serviceDefinitionOptions.ClientId,

                client_secret = serviceDefinitionOptions.ClientSecret,

                grant_type = "authorization_code",

                code = code,

                redirect_uri = serviceDefinitionOptions.RedirectUrl
            };

            request.AddObject(requestObject);

            return await notifyBotClient.PostAsync<GetTokenResult>(request);
        }

        public async Task NotifyAsync(string accessToken, string message)
        {
            var request = this.CreateRequest("notify", accessToken);

            var requestObject = new Notify()
            {
                message = message
            };

            request.AddObject(requestObject);

            await notifyApiClient.PostAsync<NotifyResult>(request);
        }
    }
}
