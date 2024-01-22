using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Further.Abp.LineNotify;

public class FurtherAbpLineNotifyAbstractionsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<ServiceDefinitionOptions>(configuration.GetSection("LineNotify:ServiceDefinition"));
    }
}
