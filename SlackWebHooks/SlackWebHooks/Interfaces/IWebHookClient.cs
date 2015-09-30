using System.Threading.Tasks;

namespace SlackWebHooks.Interfaces
{
    public interface IWebHookClient
    {
        bool SendMessage(Message message);
        Task<bool> SendMessageAsync(Message message);
    }
}
