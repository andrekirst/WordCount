using System;
using System.Linq;
using System.Collections.Generic;

namespace WordCount.Extensions
{
    public static class ListExtensions
    {
        public static List<T> ToEmptyIfNullList<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>() : list.ToList();
        }
    }
}
