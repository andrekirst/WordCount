using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WordCount.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string FirstOfMatchingRegex(
            this IEnumerable<string> enumerable,
            [StringSyntax(StringSyntaxAttribute.Regex)]string pattern) =>
            enumerable?
                .FirstOrDefault(p => p.IsMatchingRegex(pattern));
    }
}
