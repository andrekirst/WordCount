using System.Globalization;

namespace WordCount.Models.Parameters
{
    public class LanguageParameter : BaseParameter
    {
        public string Language { get; set; }

        public CultureInfo Culture { get; set; }
    }
}