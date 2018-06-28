using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCount.Extensions
{
    public static class RegexStringExtensions
    {
        public static List<string> SplitByRegex(this string text, string pattern)
        {
            MatchCollection regexMatches = Regex.Matches(
                input: text,
                pattern: pattern,
                options: RegexOptions.Compiled);

            List<string> words = regexMatches
                .OfType<Match>()
                .Select(selector: m => m.Value)
                .ToList();

            return words;
        }
    }
}
