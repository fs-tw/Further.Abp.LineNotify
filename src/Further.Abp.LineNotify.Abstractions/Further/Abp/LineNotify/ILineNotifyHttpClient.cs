using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public interface ILineNotifyHttpClient
    {
        Task<string> AuthorizeAsync(string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
        Task NotifyAsync(string message, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
        Task<string> TokenAsync(string code, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
    }
}