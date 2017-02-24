using Newtonsoft.Json;
using SlackWebHooks.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SlackWebHooks
{
    /// <summary>
    /// Client that sends messages to an Incoming WebHook Slack integration.
    /// </summary>
    public class WebHookClient : IWebHookClient
    {
        private readonly string _webhookUrl;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a WebHookClient with a endpoint url created via Slack's Incoming WebHook intergrations.
        /// </summary>
        public WebHookClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
            _httpClient = new HttpClient();
        }
        
        /// <summary>
        /// Asynchronously sends a message to the client's WebHook endpoint.
        /// </summary>
        /// <returns>True if message was successfully submitted, false otherwise.</returns>
        public Task<bool> SendMessageAsync(Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            return SendPostRequestAsync(json);
        }
        
        private async Task<bool> SendPostRequestAsync(string json)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, _webhookUrl);
            httpMessage.Content = new StringContent(json, Encoding.Unicode, "application/json");

            var httpResponse = await _httpClient.SendAsync(httpMessage);
            return httpResponse.IsSuccessStatusCode;
        }
    }
}
