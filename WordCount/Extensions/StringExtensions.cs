namespace WordCount.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(value: text);
        }
    }
}
