using Further.Abp.LineNotify;
using Microsoft.Extensions.Options;

namespace Demo.Services
{
    public class DemoApplicationService : Volo.Abp.Application.Services.ApplicationService
    {
        private readonly ILineNotifyHttpClient lineNotifyHttpClient;

        public DemoApplicationService(ILineNotifyHttpClient lineNotifyHttpClient)
        {
            this.lineNotifyHttpClient = lineNotifyHttpClient;
        }

        public async Task GetUrlAsync()
        {
            var url = await lineNotifyHttpClient.AuthorizeUrlAsync("state_yinchang");
        }

        //gEjSNakSy4ZnzEPYFui1Ia

        public async Task GetTokenAsync()
        {
            var token = await lineNotifyHttpClient.TokenAsync("gEjSNakSy4ZnzEPYFui1Ia");
        }

        //etHsSg1L8V9WgmWZCFL2Ru0Yb5TrVnDCp7IisArs8Va

        public async Task NotifyAsync()
        {
            await lineNotifyHttpClient.NotifyAsync("etHsSg1L8V9WgmWZCFL2Ru0Yb5TrVnDCp7IisArs8Va","YinChang Test");
        }
    }
}
