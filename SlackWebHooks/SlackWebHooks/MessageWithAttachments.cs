using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SlackWebHooks.Extensions;

namespace SlackWebHooks
{
    /// <summary>
    /// Message that can be sent to a slack channel via Incoming WebHook integrations.
    /// </summary>
    public class MessageWithAttachments : Message
    {
        /// <summary>
        /// Array of attachments to add to the message.
        /// </summary>
        [JsonProperty(PropertyName = "attachments", Required = Required.Always)]
        public IReadOnlyList<Attachment> Attachments { get; }

        /// <summary>
        /// Optional text that will be posted to the channel before the attachments.
        /// </summary>
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public override string Text { get; }

        /// <summary>
        /// Initializes a message with the given parameters.
        /// </summary>
        [JsonConstructor]
        public MessageWithAttachments([NotNull] IReadOnlyList<Attachment> attachments, string text = null, string username = null, string icon = null, string channel = null) : base(text?? "n/a", username, icon, channel)
        {
            Attachments = attachments;
            Text = text;
        }
    }

    /// <summary>
    /// Attachment to add to the bottom of messages.
    /// </summary>
    public class Attachment
    {
        private const string Good = "good";
        private const string Warning = "warning";
        private const string Danger = "danger";

        /// <summary>
        /// Required text summary of the attachment that is shown by clients that understand attachments but choose not to show them.
        /// </summary>
        [JsonProperty(PropertyName = "fallback", Required = Required.Always)]
        public string Fallback { get; }

        /// <summary>
        /// Optional text that should appear within the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; }

        /// <summary>
        /// Optional text that should appear above the formatted data.
        /// </summary>
        [JsonProperty(PropertyName = "pretext", NullValueHandling = NullValueHandling.Ignore)]
        public string Pretext { get; }

        /// <summary>
        /// Can either be one of 'good', 'warning', 'danger', or any hex color code.
        /// </summary>
        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        /// <summary>
        /// Fields are displayed in a table on the message.
        /// </summary>
        [JsonProperty(PropertyName = "fields", Required = Required.Always)]
        public IReadOnlyList<Field> Fields { get; } 

        /// <summary>
        /// Initializes an attachment with the given parameters.
        /// </summary>
        public Attachment([NotNull] string fallback, IReadOnlyList<Field> fields, string text = null, string pretext = null, string color = null)
        {
            Fallback = fallback;
            Fields = fields;
            Text = text;
            Pretext = pretext;

            // check color
            if (!string.IsNullOrWhiteSpace(color))
            {
                if (Good.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                    Color = Good;
                else if (Warning.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                    Color = Warning;
                else if (Danger.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                    Color = Danger;
                else if (color.IsValidHexColor())
                    Color = color;
                else
                    throw new ArgumentOutOfRangeException(nameof(color), "Color must be either 'good', 'bad', or a hex color in the form of '#RRGGBB'.");
            }
            else
                Color = null;
        }
    }

    /// <summary>
    /// Field that displays text with a title on an attachment.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Required Field Title. The title may not contain markup and will be escaped for you.
        /// </summary>
        [JsonProperty(PropertyName = "title", Required = Required.Always)]
        public string Title { get; }

        /// <summary>
        /// Text value of the field. May contain standard message markup and must be escaped as normal. May be multi-line.
        /// </summary>
        [JsonProperty(PropertyName = "value", Required = Required.Always)]
        public string Value { get; }

        /// <summary>
        /// Optional flag indicating whether the `value` is short enough to be displayed side-by-side with other values.
        /// </summary>
        [JsonProperty(PropertyName = "short", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Short { get; }

        /// <summary>
        /// Initializes a field with the given parameters.
        /// </summary>
        public Field(string title, string value, bool? @short = null)
        {
            Title = title;
            Value = value;
            Short = @short;
        }
    }
}
