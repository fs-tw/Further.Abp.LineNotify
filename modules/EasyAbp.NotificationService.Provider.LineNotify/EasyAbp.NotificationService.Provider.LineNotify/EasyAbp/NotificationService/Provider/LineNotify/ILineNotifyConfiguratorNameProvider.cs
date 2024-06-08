using System;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Provider.LineNotify
{
    public interface ILineNotifyConfiguratorNameProvider
    {
        Task<string> GetAsync(Guid userId);
    }
}