using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCount.Extensions
{
    public static class RegexStringExtensions
    {
        public static List<string> SplitByRegex(this string text, string pattern)
        {
            return Regex.Matches(
                input: text,
                pattern: pattern,
                options: RegexOptions.Compiled)
                .OfType<Match>()
                .Select(selector: m => m.Value)
                .ToList();
        }

        public static bool IsMatchingRegex(this string text, string pattern)
        {
            if (text.IsNullOrEmpty() || pattern.IsNullOrEmpty())
            {
                return false;
            }
            return Regex.IsMatch(
                input: text,
                pattern: pattern);
        }
    }
}
