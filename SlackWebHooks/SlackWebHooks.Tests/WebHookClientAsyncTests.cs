using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SlackWebHooks.Tests
{
    [TestFixture]
    public class WebHookClientAsyncTests
    {
        private const string HookUrl = "https://hooks.slack.com/services/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";
        private const string IconUrl = "https://slack.com/img/icons/app-57.png";
        private const string Emoji = ":scream:";

        [Test]
        public async Task SendMessageAsync_Test()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message");
            await client.SendMessageAsync(message);
        }

        [Test]
        public async Task SendMessageAsync_WithUsernameTest()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with username", username: "NotSam");
            await client.SendMessageAsync(message);
        }

        [Test]
        public async Task SendMessageAsync_WithIconUrl()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon url", icon: IconUrl);
            await client.SendMessageAsync(message);
        }

        [Test]
        public async Task SendMessageAsync_WithIconEmoji()
        {
            var client = new WebHookClient(HookUrl);
            var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon emoji", icon: Emoji);
            await client.SendMessageAsync(message);
        }
    }
}
