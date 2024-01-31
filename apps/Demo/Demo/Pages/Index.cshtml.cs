using Demo.Services;
using Further.Abp.LineNotify;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Demo.Pages;

public class IndexModel : AbpPageModel
{
    public IndexModel(DemoApplicationService demoApplicationService)
    {
        //demoApplicationService.GetUrlAsync();

        //demoApplicationService.NotifyAsync();
    }
}