using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.NotificationService.Provider.LineNotify;

public static class CreateNotificationInfoModelExtensions
{
    public static void SetSubject(this CreateNotificationInfoModel model, [NotNull] string subject)
    {
        model.SetProperty(nameof(CreateLineNotifyNotificationEto.Subject), subject);
    }

    public static string GetSubject(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateLineNotifyNotificationEto.Subject));
    }

    public static void SetBody(this CreateNotificationInfoModel model, [CanBeNull] string body)
    {
        model.SetProperty(nameof(CreateLineNotifyNotificationEto.Body), body);
    }

    public static string GetBody(this CreateNotificationInfoModel model)
    {
        return (string)model.GetProperty(nameof(CreateLineNotifyNotificationEto.Body));
    }
}