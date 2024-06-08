using EasyAbp.NotificationService.Options;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.LineNotify;

[DependsOn(
    typeof(EasyAbp.NotificationService.NotificationServiceDomainModule),
    typeof(LineNotifyAbstractionsModule)
    )]
public class LineNotifyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(
                NotificationProviderLineNotifyConsts.NotificationMethod, typeof(LineNotifyNotificationManager)));
        });
    }
}