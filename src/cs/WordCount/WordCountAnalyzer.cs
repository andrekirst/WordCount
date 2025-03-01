using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount;

public interface IWordCountAnalyzer
{
    WordCountAnalyzerResult Analyze(string text);
}

public class WordCountAnalyzer(
    ITextSplit textSplit,
    IStopwordRemover stopwordRemover) : IWordCountAnalyzer
{
    public WordCountAnalyzerResult Analyze(string text)
    {
        var textSplitResult = textSplit.Split(text);

        if (!textSplitResult.WordsAvailable)
        {
            return new WordCountAnalyzerResult();
        }

        var stopwordRemoverResult = stopwordRemover.RemoveStopwords(textSplitResult.Words);

        var words = stopwordRemoverResult.Words;
        var distinctWords = words.Distinct().ToList();

        var numberOfWords = words.Count;
        var numberOfUniqueWords = distinctWords.Count;
        var averageWordLength = words.Count != 0 ? words.Average(s => s.Length) : 0.0;
        var numberOfchapters = text.Split([Environment.NewLine + Environment.NewLine], StringSplitOptions.None).Length;

        return new WordCountAnalyzerResult
        {
            NumberOfWords = numberOfWords,
            NumberOfUniqueWords = numberOfUniqueWords,
            AverageWordLength = averageWordLength,
            DistinctWords = distinctWords,
            NumberOfChapters = numberOfchapters
        };
    }
}

public class WordCountAnalyzerResult
{
    public int NumberOfWords { get; set; }

    public int NumberOfUniqueWords { get; set; }

    public double AverageWordLength { get; set; }

    public List<string> DistinctWords { get; set; }

    public int NumberOfChapters { get; set; }
}
