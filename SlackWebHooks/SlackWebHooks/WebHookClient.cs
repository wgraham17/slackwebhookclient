using System.IO;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SlackWebHooks.Interfaces;

namespace SlackWebHooks
{
    /// <summary>
    /// Client that sends messages to an Incoming WebHook Slack integration.
    /// </summary>
    public class WebHookClient : IWebHookClient
    {
        private readonly string _webhookUrl;

        /// <summary>
        /// Initializes a WebHookClient with a endpoint url created via Slack's Incoming WebHook intergrations.
        /// </summary>
        public WebHookClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        /// <summary>
        /// Sends a message to the client's WebHook endpoint.
        /// </summary>
        /// <returns>True if message was successfully submitted, false otherwise.</returns>
        public bool SendMessage([NotNull] Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            return SendPostRequest(json);
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
        
        private bool SendPostRequest(string json)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(_webhookUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            return httpResponse.StatusCode == HttpStatusCode.Accepted;
            /*
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            */
        }

        private async Task<bool> SendPostRequestAsync(string json)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(_webhookUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                await streamWriter.WriteAsync(json);
            }

            var httpResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            return httpResponse.StatusCode == HttpStatusCode.Accepted;
            /*
            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            */
        }
    }
}
