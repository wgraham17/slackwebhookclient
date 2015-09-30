using System;

namespace SlackWebHooks.Extensions
{
    public static class Extensions
    {
        public static string LinkifyUri(this Uri uri)
        {
            return $"<{uri.AbsoluteUri}>";
        }

        public static string LinkifyUriWithText(this Uri uri, string textToDisplay)
        {
            if(string.IsNullOrWhiteSpace(textToDisplay))
                throw new ArgumentNullException(nameof(textToDisplay));

            return $"{uri.AbsoluteUri}|{textToDisplay}";
        }

        public static bool IsValidEmoji(this string emoji)
        {
            return emoji.StartsWith(":") && emoji.EndsWith(":");
        }

        public static bool IsValidChannel(this string channel)
        {
            return channel.StartsWith("#") || channel.StartsWith("@");
        }
    }
}
