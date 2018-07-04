using System.Collections.Generic;
using System.Linq;

namespace WordCount.Extensions
{
    public static class EnumerableStringExtensions
    {
        public static string FirstOfMatchingRegex(this IEnumerable<string> enumerable, string pattern)
        {
            return enumerable?
                .FirstOrDefault(predicate: p => p.IsMatchingRegex(pattern: pattern));
        }
    }
}
