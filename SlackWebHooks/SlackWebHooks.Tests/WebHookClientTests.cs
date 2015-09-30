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
                var attachment1 = new Attachment("fallback", new [] {field1}, "goog", "pretext", "good");
                var attachment2 = new Attachment("fallback", new[] { field1 }, "warn", "pretext", "warning");
                var attachment3 = new Attachment("fallback", new[] { field1 }, "dager", "pretext", "danger");
                var attachment4 = new Attachment("fallback", new[] { field1 }, "colur", "pretext", "#0000FF");
                var messageWithAttachments = new MessageWithAttachments(new[] { attachment1, attachment2, attachment3, attachment4});
                var client = new WebHookClient(HookUrl);
                client.SendMessage(messageWithAttachments);
            }
        }
    }
}
