using System;
using NUnit.Framework;

namespace SlackWebHooks.Tests
{
    [TestFixture]
    public class WebHookClientTests
    {
        private const string HookUrl = "https://hooks.slack.com/services/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";
        private const string IconUrl = "https://slack.com/img/icons/app-57.png";
        private const string Emoji = ":scream:";

        public class MessageTests
        {
            [Test]
            public void SendMessage_Test()
            {
                var client = new WebHookClient(HookUrl);
                var message = new Message($"[{DateTime.UtcNow}]\tTest message");
                client.SendMessage(message);
            }

            [Test]
            public void SendMessage_WithUsernameTest()
            {
                var client = new WebHookClient(HookUrl);
                var message = new Message($"[{DateTime.UtcNow}]\tTest message with username", username: "NotSam");
                client.SendMessage(message);
            }

            [Test]
            public void SendMessage_WithIconUrl()
            {
                var client = new WebHookClient(HookUrl);
                var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon url", icon: IconUrl);
                client.SendMessage(message);
            }

            [Test]
            public void SendMessage_WithIconEmoji()
            {
                var client = new WebHookClient(HookUrl);
                var message = new Message($"[{DateTime.UtcNow}]\tTest message with icon emoji", icon: Emoji);
                client.SendMessage(message);
            }
        }

        public class MessageWithAttachmentsTests
        {
            [Test]
            public void Test()
            {
                var field1 = new Field("title 1", "value 1", false);
                var field2 = new Field("title 2", "value 2", false);
                var attachment = new Attachment("fallback", new [] {field1, field2}, "text", "pretext", "bad");
                var messageWithAttachments = new MessageWithAttachments(new[] { attachment});
                var client = new WebHookClient(HookUrl);
                client.SendMessage(messageWithAttachments);
            }
        }
    }
}
