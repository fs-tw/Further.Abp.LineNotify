using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.LineNotify;

[DependsOn(
    typeof(NotificationServiceDomainSharedModule)
    )]
public class LineNotifyAbstractionsModule : AbpModule
{

}