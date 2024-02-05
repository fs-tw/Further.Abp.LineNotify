using Further.Abp.LineNotify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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
        private readonly IAccessTokenProvider accessTokenProvider;

        public LineNotifyController(
            IOptions<LineNotifyOptions> options,
            ILineNotifyHttpClient lineNotifyHttpClient,
            IDistributedEventBus distributedEventBus,
            IAccessTokenProvider accessTokenProvider)
        {
            this.options = options.Value;
            this.lineNotifyHttpClient = lineNotifyHttpClient;
            this.distributedEventBus = distributedEventBus;
            this.accessTokenProvider = accessTokenProvider;
        }

        [HttpGet("authorize-url")]
        public async Task<string> AuthorizeUrlAsync(string resultUrl, string groupName = LineNotifyConsts.DefaultGroupName, string configuratorName = LineNotifyConsts.DefaultConfiguratorName)
        {
            var url = await lineNotifyHttpClient.AuthorizeAsync(resultUrl, groupName, configuratorName);
            return url;
        }

        [HttpGet("token")]
        public async Task<string> TokenAsync(string code, string configuratorName = LineNotifyConsts.DefaultConfiguratorName)
        {
            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorName);
            return token;
        }

        [HttpGet("notify")]
        public async Task NotifyAsync(string message, string configuratorName = LineNotifyConsts.DefaultConfiguratorName)
        {
            await lineNotifyHttpClient.NotifyAsync(message, configuratorName);
        }

        [HttpGet("callback")]
        public async Task<RedirectResult> CallbackAsync(string code, string state)
        {
            var configuratorName = state.Split('_')[0];

            var groupName = state.Split('_')[1];

            var resultUrl = state.Split('_')[2];

            var token = await lineNotifyHttpClient.TokenAsync(code, configuratorName);

            await accessTokenProvider.SetAccessTokenAsync(configuratorName, groupName, token);

            await distributedEventBus.PublishAsync(new LoginCallBackEto
            {
                State = state,
                ConfiguratorsName = configuratorName,
                GroupName = groupName,
                Code = code,
                Token = token,
                ResultUrl = resultUrl
            });

            resultUrl = $"{resultUrl}?groupName={groupName}&configuratorName={configuratorName}";

            return Redirect(new Uri(resultUrl).ToString());

        }
    }
}
