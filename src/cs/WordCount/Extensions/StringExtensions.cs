using System;
using System.Diagnostics.CodeAnalysis;

namespace WordCount.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text) => string.IsNullOrWhiteSpace(text);

        public static bool IsFilled([NotNull]this string text) => !string.IsNullOrWhiteSpace(text);

        public static bool IsValidUrl(this string text) => Uri.IsWellFormedUriString(text, UriKind.Absolute);

        public static string FillRightWithPoints(this string text, int totalWidth) =>
            text.PadRight(totalWidth, '.');
    }
}
