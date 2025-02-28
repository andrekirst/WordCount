using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCount.Extensions
{
    public static class RegexStringExtensions
    {
        public static List<string> SplitByRegex(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern) =>
            Regex.Matches(
                    text,
                    pattern,
                    RegexOptions.Compiled)
                .OfType<Match>()
                .Select(m => m.Value)
                .ToList();

        public static bool IsMatchingRegex(this string text, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            if (text.IsNullOrEmpty() || pattern.IsNullOrEmpty())
            {
                return false;
            }
            return Regex.IsMatch(
                text,
                pattern);
        }
    }
}
