using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.Logging;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.LineNotify;

public class LineNotifyNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod => NotificationProviderLineNotifyConsts.NotificationMethod;

    //protected IEmailSender EmailSender => LazyServiceProvider.LazyGetRequiredService<IEmailSender>();

    protected ILineNotifyConfiguratorNameProvider UserLineNotifyConfiguratorNameProvider =>
        LazyServiceProvider.LazyGetRequiredService<ILineNotifyConfiguratorNameProvider>();

    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
       throw new NotImplementedException();
        //var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        //notificationInfo.SetMailingData(model.GetSubject(), model.GetBody());

        //var notifications = await CreateNotificationsAsync(notificationInfo, model);

        //return (notifications, notificationInfo);
    }

    [UnitOfWork]
    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var configuratorName = await UserLineNotifyConfiguratorNameProvider.GetAsync(notification.UserId);

        if (configuratorName.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(
                notification, false, NotificationConsts.FailureReasons.ReceiverInfoNotFound);

            return;
        }

        try
        {
            //await EmailSender.SendAsync(userEmailAddress,
            //    notificationInfo.GetMailingSubject(),
            //    notificationInfo.GetMailingBody());

            await SetNotificationResultAsync(notification, true);
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            var message = e is IHasErrorCode b ? b.Code ?? e.Message : e.ToString();
            await SetNotificationResultAsync(notification, false, message);
        }
    }
}