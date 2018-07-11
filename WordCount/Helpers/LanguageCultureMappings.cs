using System.Collections.Generic;

namespace WordCount.Helpers
{
    public static class LanguageCultureMappings
    {
        public static readonly Dictionary<string, string> Mappings = new Dictionary<string, string>()
        {
            { "en", "en-US" },
            { "de", "de-DE" }
        };
    }
}