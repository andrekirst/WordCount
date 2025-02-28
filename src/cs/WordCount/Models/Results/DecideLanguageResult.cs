using System.Globalization;

namespace WordCount.Models.Results;

public class DecideLanguageResult
{
    public string Language { get; set; }

    public CultureInfo Culture { get; set; }
}
