using System;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations;

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
