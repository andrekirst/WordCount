using System.Collections.Generic;
using System.Globalization;

namespace WordCount.Helpers;

public static class LanguageToCultureMapping
{
    internal static readonly Dictionary<string, CultureInfo> Mappings = new()
    {
        { Languages.English, CultureInfo.GetCultureInfo("en-US") },
        { Languages.German, CultureInfo.GetCultureInfo("de-DE") }
    };
}