using System.Collections.Generic;
using System.Linq;

namespace WordCount.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string FirstOfMatchingRegex(this IEnumerable<string> enumerable, string pattern) =>
            enumerable?
                .FirstOrDefault(p => p.IsMatchingRegex(pattern));
    }
}
