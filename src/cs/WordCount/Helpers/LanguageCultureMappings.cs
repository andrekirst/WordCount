using System.Collections.Generic;
using System.Globalization;

namespace WordCount.Helpers
{
    public static class LanguageToCultureMapping
    {
        internal static readonly Dictionary<string, CultureInfo> Mappings = new()
        {
            { "en", CultureInfo.GetCultureInfo("en-US") },
            { "de", CultureInfo.GetCultureInfo("de-DE") }
        };
    }
}