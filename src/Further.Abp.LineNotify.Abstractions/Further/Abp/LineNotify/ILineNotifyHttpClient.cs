using System.Threading.Tasks;

namespace Further.Abp.LineNotify
{
    public interface ILineNotifyHttpClient
    {
        Task<string> AuthorizeUrlAsync(string state);
        Task NotifyAsync(string accessToken, string message);
        Task<string> TokenAsync(string code);
    }
}