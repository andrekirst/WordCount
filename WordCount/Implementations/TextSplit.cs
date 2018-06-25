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
                return new TextSplitResult(values: new List<string>());
            }

            // Regulärer Ausdruck, der Alle zeichen (inklusive - und Unicode-Zeichen) sucht und kein - am Ende haben
            MatchCollection regexMatches = Regex.Matches(
                input: text,
                pattern: @"[a-zA-Z\-\u00D8-\u00F6]{0,}[^-\s\d]",
                options: RegexOptions.Compiled);

            List<string> splitByRegex = regexMatches
                .OfType<Match>()
                .Select(selector: m => m.Value)
                .ToList();

            return new TextSplitResult(values: splitByRegex);
        }
    }
}
