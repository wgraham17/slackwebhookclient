using System.IO;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SlackWebHooks.Interfaces;

namespace SlackWebHooks
{
    public class WebHookClient : IWebHookClient
    {
        private readonly string _webhookUrl;

        public WebHookClient(string webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public bool SendMessage([NotNull] Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            return SendPostRequest(json);
        }

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
