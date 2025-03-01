using System.Collections.Generic;
using WordCount.Extensions;

namespace WordCount;

// TODO Nicht als Schnittstelle, sondern als Extension?
public interface ITextSplit
{
    TextSplitResult Split(string text);
}

public class TextSplit : ITextSplit
{
    public TextSplitResult Split(string text)
    {
        if (text.IsNullOrEmpty())
        {
            return new TextSplitResult();
        }

        var words = text.SplitByRegex(@"((\b[^\s\d]+\b)((?<=\.\w).)?)");
        return new TextSplitResult(words);
    }
}

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
