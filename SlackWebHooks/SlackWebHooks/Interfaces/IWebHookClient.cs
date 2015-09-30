using System.Threading.Tasks;

namespace SlackWebHooks.Interfaces
{
    /// <summary>
    /// Interface for a WebHookClient
    /// </summary>
    public interface IWebHookClient
    {
        /// <summary>
        /// Sends a message to the client's WebHook endpoint.
        /// </summary>
        /// <returns>True if message was successfully submitted, false otherwise.</returns>
        bool SendMessage(Message message);

        /// <summary>
        /// Asynchronously sends a message to the client's WebHook endpoint.
        /// </summary>
        /// <returns>True if message was successfully submitted, false otherwise.</returns>
        Task<bool> SendMessageAsync(Message message);
    }
}
