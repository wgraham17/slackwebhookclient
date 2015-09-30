using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SlackWebHooks.Extensions;

namespace SlackWebHooks
{
    /// <summary>
    /// Message that can be sent to a slack channel via Incoming WebHook integrations.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// This is the text that will be posted to the channel.
        /// </summary>
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public virtual string Text { get; }

        /// <summary>
        /// The username you want your WebHook to post under.
        /// </summary>
        [JsonProperty(PropertyName = "username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; }

        /// <summary>
        /// Url to an image you want your WebHook to use as an icon.
        /// </summary>
        [JsonProperty(PropertyName = "icon_url", NullValueHandling = NullValueHandling.Ignore)]
        public string IconUrl { get; }

        /// <summary>
        /// Emoji in the form :emoji: that you want your WebHook to use as an Icon.
        /// </summary>
        [JsonProperty(PropertyName = "icon_emoji", NullValueHandling = NullValueHandling.Ignore)]
        public string IconEmoji { get; }

        /// <summary>
        /// The channel you want your WebHook to post the message in.
        /// </summary>
        [JsonProperty(PropertyName = "channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; }

        /// <summary>
        /// Initializes a message with the given parameters.
        /// </summary>
        public Message([NotNull] string text, string username = null, string icon = null, string channel = null)
        {
            Text = text;
            Username = username;

            // check url
            if (!string.IsNullOrWhiteSpace(icon))
            {
                Uri _;
                if (Uri.TryCreate(icon, UriKind.Absolute, out _))
                {
                    IconUrl = icon;
                    IconEmoji = null;
                }
                else if (icon.IsValidEmoji())
                {
                    IconEmoji = icon;
                    IconUrl = null;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(icon), "Icon must be either valid url or ':emoji:'.");
                }
            }
            else
            {
                IconUrl = null;
                IconEmoji = null;
            }

            // check channel
            if (!string.IsNullOrWhiteSpace(channel))
            {
                if (channel.IsValidChannel())
                {
                    Channel = channel;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(channel), "Channel must start with either '#' or '@'.");
                }
            }
            else
            {
                Channel = null;
            }
        }
    }
}
