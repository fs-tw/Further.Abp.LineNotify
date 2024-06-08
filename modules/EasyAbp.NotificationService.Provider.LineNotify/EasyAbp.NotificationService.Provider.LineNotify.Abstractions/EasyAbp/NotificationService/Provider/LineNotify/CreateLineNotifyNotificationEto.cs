using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.LineNotify;

[Serializable]
public class CreateLineNotifyNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    [NotNull]
    public string Subject
    {
        get => this.GetSubject();
        set => this.SetSubject(value);
    }

    [CanBeNull]
    public string Body
    {
        get => this.GetBody();
        set => this.SetBody(value);
    }

    public CreateLineNotifyNotificationEto()
    {
    }

    //public CreateLineNotifyNotificationEto(
    //    Guid? tenantId,
    //    IEnumerable<NotificationUserInfoModel> users,
    //    [NotNull] string subject,
    //    [CanBeNull] string body) : base(NotificationProviderLineNotifyConsts.NotificationMethod, users)
    //{
    //    TenantId = tenantId;
    //    Subject = subject;
    //    Body = body;
    //}

    public CreateLineNotifyNotificationEto(
        Guid? tenantId,
        IEnumerable<Guid> userIds,
        [NotNull] string subject,
        [CanBeNull] string body) : base(NotificationProviderLineNotifyConsts.NotificationMethod, userIds)
    {
        TenantId = tenantId;
        Subject = subject;
        Body = body;
    }

    //public CreateLineNotifyNotificationEto(
    //    Guid? tenantId,
    //    NotificationUserInfoModel user,
    //    [NotNull] string subject,
    //    [CanBeNull] string body) : base(NotificationProviderLineNotifyConsts.NotificationMethod, user)
    //{
    //    TenantId = tenantId;
    //    Subject = subject;
    //    Body = body;
    //}

    public CreateLineNotifyNotificationEto(
        Guid? tenantId,
        Guid userId,
        [NotNull] string subject,
        [CanBeNull] string body) : base(NotificationProviderLineNotifyConsts.NotificationMethod, userId)
    {
        TenantId = tenantId;
        Subject = subject;
        Body = body;
    }
}