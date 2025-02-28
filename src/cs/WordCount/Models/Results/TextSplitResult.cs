using System.Collections.Generic;

namespace WordCount.Models.Results;

public class TextSplitResult
{
    public TextSplitResult()
    {
    }

    public TextSplitResult(List<string> words)
    {
        Words = words;
    }

    public List<string> Words { get; } = [];

    public bool WordsAvailable => Words != null && Words.Count != 0;
}
