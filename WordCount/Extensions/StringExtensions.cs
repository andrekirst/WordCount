using System;

namespace WordCount.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(value: text);
        }

        public static bool IsFilled(this string text)
        {
            return !string.IsNullOrWhiteSpace(value: text);
        }
    }
}
