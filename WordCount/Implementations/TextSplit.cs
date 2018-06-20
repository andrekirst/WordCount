using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class TextSplit : ITextSplit
    {
        public TextSplitResult Split(string text)
        {
            if (string.IsNullOrWhiteSpace(value: text))
            {
                return new TextSplitResult(new List<string>());
            }

            MatchCollection regexMatches = Regex.Matches(
                input: text,
                pattern: @"[a-zA-Z\-\u00D8-\u00F6]{1,}",
                options: RegexOptions.Compiled);

            List<string> splitByRegex = regexMatches
                .OfType<Match>()
                .Select(m => m.Value)
                .ToList();

            return new TextSplitResult(values: splitByRegex);
        }
    }
}
