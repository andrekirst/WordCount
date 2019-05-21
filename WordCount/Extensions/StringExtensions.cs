using System;

namespace WordCount.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text) => string.IsNullOrWhiteSpace(value: text);

        public static bool IsFilled(this string text) => !string.IsNullOrWhiteSpace(value: text);

        public static bool IsValidUrl(this string text) => Uri.IsWellFormedUriString(uriString: text, uriKind: UriKind.Absolute);

        public static string FillRightWithPoints(this string text, int totalWidth) =>
            text.PadRight(
                totalWidth: totalWidth,
                paddingChar: '.');
    }
}
