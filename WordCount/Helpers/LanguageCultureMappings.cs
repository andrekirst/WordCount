using System.Collections.Generic;
using System.Globalization;

namespace WordCount.Helpers
{
    public class LanguageCultureMappings
    {
        public static Dictionary<string, string> Mappings = new Dictionary<string, string>()
        {
            { "en", "en-US" },
            { "de", "de-DE" }
        };
    }
}