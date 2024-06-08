using EasyAbp.NotificationService.Options;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.LineNotify;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(LineNotifyAbstractionsModule)
)]
public class NotificationServiceProviderLineNotifyModule : AbpModule
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