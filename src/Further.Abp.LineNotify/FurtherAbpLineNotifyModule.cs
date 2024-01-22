using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Further.Abp.LineNotify;

[DependsOn(
       typeof(FurtherAbpLineNotifyAbstractionsModule)
    )]
public class FurtherAbpLineNotifyModule : AbpModule
{
}
