using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
