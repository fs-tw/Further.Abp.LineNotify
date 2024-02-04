using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public interface ILineNotifyHttpClient
    {
        Task<string> AuthorizeUrlAsync(string state, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
        Task NotifyAsync(string accessToken, string message, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
        Task<string> TokenAsync(string code, string configuratorsName = LineNotifyOptions.DefaultConfiguratorName);
    }
}