namespace SlackWebHooks.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class WebHookClientAsyncTests
    {
        private const string HookUrl = "https://hooks.slack.com/services/replaceme";
        private const string IconUrl = "https://slack.com/img/icons/app-57.png";
        private const string Emoji = ":scream:";

        [TestMethod]
        public async Task SendMessageAsync_Test()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message");
            await client.SendMessageAsync(message);
        }

        [TestMethod]
        public async Task SendMessageAsync_WithUsernameTest()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with username", username: "NotSam");
            await client.SendMessageAsync(message);
        }

        [TestMethod]
        public async Task SendMessageAsync_WithIconUrl()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon url", icon: IconUrl);
            await client.SendMessageAsync(message);
        }

        [TestMethod]
        public async Task SendMessageAsync_WithIconEmoji()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon emoji", icon: Emoji);
            await client.SendMessageAsync(message);
        }
    }
}
