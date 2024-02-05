using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public interface ILineNotifyHttpClient
    {
        Task<string> AuthorizeAsync(string resultUrl, string configuratorName = LineNotifyConsts.DefaultConfiguratorName, string groupName = LineNotifyConsts.DefaultGroupName);
        Task NotifyAsync(string message, string configuratorName = LineNotifyConsts.DefaultConfiguratorName, string groupName = LineNotifyConsts.DefaultGroupName);
        Task<string> TokenAsync(string code, string configuratorName = LineNotifyConsts.DefaultConfiguratorName);
    }
}