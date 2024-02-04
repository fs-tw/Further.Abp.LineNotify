using Further.Abp.LineNotify;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;

namespace Further.Abp.LineNotify.Controllers
{
    [Area("AbpLineNotify")]
    [RemoteService(Name = "AbpLineNotify")]
    [Route("api/line-notify")]
    public class LineNotifyController : AbpControllerBase, IRemoteService
    {
        private readonly ILineNotifyHttpClient lineNotifyHttpClient;
        private readonly IDistributedEventBus distributedEventBus;

        public LineNotifyController(
            ILineNotifyHttpClient lineNotifyHttpClient,
            IDistributedEventBus distributedEventBus)
        {
            this.lineNotifyHttpClient = lineNotifyHttpClient;
            this.distributedEventBus = distributedEventBus;
        }

        [HttpGet("authorize-url")]
        public async Task<string> AuthorizeUrlAsync(string state, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName)
        {
            var url = await lineNotifyHttpClient.AuthorizeUrlAsync(state, configuratorsName);
            return url;
        }

        [HttpGet("token")]
        public async Task<string> TokenAsync(string code, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorsName);
            return token;
        }

        [HttpGet("notify")]
        public async Task NotifyAsync(string token, string message, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName)
        {
            await lineNotifyHttpClient.NotifyAsync(token, message, configuratorsName);
        }

        [HttpGet("callback")]
        public async Task CallbackAsync(string code, string state, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorsName);

            await distributedEventBus.PublishAsync(new LoginCallBackEto
            {
                ConfiguratorsName = configuratorsName,
                Code = code,
                State = state,
                Token = token
            });
        }
    }
}
