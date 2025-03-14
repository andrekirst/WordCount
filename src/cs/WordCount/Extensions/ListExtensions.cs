﻿using System.Linq;
using System.Collections.Generic;

namespace WordCount.Extensions;

public static class ListExtensions
{
    public static List<T> ToEmptyIfNullList<T>(this IEnumerable<T> list) => list == null ? [] : list.ToList();
}
