using Further.Abp.LineNotify;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.EventBus.Distributed;

namespace Demo.Controllers
{
    public class LineNotifyDemoController: AbpControllerBase, IRemoteService
    {
        private readonly ILineNotifyHttpClient lineNotifyHttpClient;
        private readonly IDistributedEventBus distributedEventBus;

        public LineNotifyDemoController(
            ILineNotifyHttpClient lineNotifyHttpClient,
            IDistributedEventBus distributedEventBus)
        {
            this.lineNotifyHttpClient = lineNotifyHttpClient;
            this.distributedEventBus = distributedEventBus;
        }

        public async Task<string> GetAuthorizeUrlAsync(string state)
        {
            var url = await lineNotifyHttpClient.AuthorizeUrlAsync(state);
            return url;
        }

        public async Task<string> GetTokenAsync(string code)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code);
            return token;
        }

        public async Task NotifyAsync(string token, string message)
        {
            await lineNotifyHttpClient.NotifyAsync(token, message);
        }

        [HttpGet("line-notify-callback")]
        public async Task LineNotifyCallBackAsync(string code, string state)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code);

            await distributedEventBus.PublishAsync(new LoginCallBackEto
            {
                Code = code,
                State = state,
                Token = token
            });
        }
    }
}
