using Further.Abp.LineNotify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;

namespace Further.Abp.LineNotify.Controllers
{
    [Area("LineNotify")]
    [RemoteService(Name = "LineNotify")]
    [Route("api/line-notify")]
    public class LineNotifyController : AbpControllerBase, IRemoteService
    {
        private readonly LineNotifyOptions options;
        private readonly ILineNotifyHttpClient lineNotifyHttpClient;
        private readonly IDistributedEventBus distributedEventBus;

        public LineNotifyController(
            IOptions<LineNotifyOptions> options,
            ILineNotifyHttpClient lineNotifyHttpClient,
            IDistributedEventBus distributedEventBus)
        {
            this.options = options.Value;
            this.lineNotifyHttpClient = lineNotifyHttpClient;
            this.distributedEventBus = distributedEventBus;
        }

        [HttpGet("authorize-url")]
        public async Task<string> AuthorizeUrlAsync(string configuratorName = LineNotifyOptions.DefaultConfiguratorName)
        {
            var url = await lineNotifyHttpClient.AuthorizeAsync(configuratorName);
            return url;
        }

        [HttpGet("token")]
        public async Task<string> TokenAsync(string code, string configuratorName = LineNotifyOptions.DefaultConfiguratorName)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorName);
            return token;
        }

        [HttpGet("notify")]
        public async Task NotifyAsync(string message, string configuratorName = LineNotifyOptions.DefaultConfiguratorName)
        {
            await lineNotifyHttpClient.NotifyAsync(message, configuratorName);
        }

        [HttpGet("callback")]
        public async Task CallbackAsync(string code, string state)
        {
            var configuratorName = state;

            var configurator = options.Configurators[configuratorName];

            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorName);

            configurator.SetAccessToken(token);

            await distributedEventBus.PublishAsync(new LoginCallBackEto
            {
                ConfiguratorsName = configuratorName,
                Code = code,
                State = state,
                Token = token
            });
        }
    }
}
