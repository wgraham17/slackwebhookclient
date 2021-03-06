﻿using System;
using System.Text.RegularExpressions;

namespace SlackWebHooks.Extensions
{
    /// <summary>
    /// Slack-related extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Turns a uri into a clickable string that can be used in message text fields.  
        /// The text to display instead of the url can be optionally set.
        /// </summary>
        public static string LinkifyUri(this Uri uri, string textToDisplay = null)
        {
            return string.IsNullOrWhiteSpace(textToDisplay) ? $"<{uri.AbsoluteUri}>" : $"{uri.AbsoluteUri}|{textToDisplay}";
        }

        /// <summary>
        /// Tests to see if a string fits the format for a Slack emoji.
        /// </summary>
        public static bool IsValidEmoji(this string emoji)
        {
            return emoji.StartsWith(":") && emoji.EndsWith(":");
        }

        /// <summary>
        /// Tests to see if a string fits the format for a Slack channel (or direct message).
        /// </summary>
        public static bool IsValidChannel(this string channel)
        {
            return channel.StartsWith("#") || channel.StartsWith("@");
        }

        /// <summary>
        /// Checks if a string is a valid hex color (#RRGGBB).
        /// </summary>
        public static bool IsValidHexColor(this string color)
        {
            const string rgx = "^#[a-fA-F0-9]{6}$";
            var regex = new Regex(rgx);
            var match = regex.Match(color);
            return match.Success;
        }
    }
}
