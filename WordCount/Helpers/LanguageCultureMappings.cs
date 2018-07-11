using System.Collections.Generic;

namespace WordCount.Helpers
{
    public static class LanguageCultureMappings
    {
        internal static readonly Dictionary<string, string> Mappings = new Dictionary<string, string>()
        {
            { "en", "en-US" },
            { "de", "de-DE" }
        };
    }
}