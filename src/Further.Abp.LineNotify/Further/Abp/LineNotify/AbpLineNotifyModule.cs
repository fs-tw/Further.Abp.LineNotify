using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Further.Abp.LineNotify;

[DependsOn(
    typeof(AbpLineNotifyAbstractionsModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class AbpLineNotifyModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var lineNotifyOptions = context.Services.BuildServiceProvider().GetRequiredService<IOptions<LineNotifyOptions>>();

        lineNotifyOptions.Value.Configurators.ToList().ForEach(configurator =>
        {
            context.Services.AddHttpClient($"LineNotify_{configurator.Key}", options =>
            {
                //options.BaseAddress = new Uri(lineNotifyOptions.Value.NotifyApiUrl);
                //options.DefaultRequestHeaders.Add("Authorization", $"Bearer {configurator.Value.AccessToken}");
            });
        });

    }
}
