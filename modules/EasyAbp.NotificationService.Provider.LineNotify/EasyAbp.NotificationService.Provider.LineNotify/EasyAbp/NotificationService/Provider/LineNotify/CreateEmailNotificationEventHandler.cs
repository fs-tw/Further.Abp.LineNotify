using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.LineNotify;

public class CreateEmailNotificationEventHandler : IDistributedEventHandler<CreateLineNotifyNotificationEto>,
    ITransientDependency
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly LineNotifyNotificationManager _emailNotificationManager;

    public CreateEmailNotificationEventHandler(
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        LineNotifyNotificationManager emailNotificationManager)
    {
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _emailNotificationManager = emailNotificationManager;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateLineNotifyNotificationEto eventData)
    {
        var result = await _emailNotificationManager.CreateAsync(eventData);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }
    }
}