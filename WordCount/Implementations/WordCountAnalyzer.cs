using System;
using System.Linq;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        private ITextSplit TextSplit { get; }
        private IStopwordRemover StopwordRemover { get; }

        public WordCountAnalyzer(
            ITextSplit textSplit,
            IStopwordRemover stopwordRemover)
        {
            TextSplit = textSplit;
            StopwordRemover = stopwordRemover;
        }

        public WordCountAnalyzerResult Analyze(string text)
        {
            var textSplitResult = TextSplit.Split(text);

            if (!textSplitResult.WordsAvailable)
            {
                return new WordCountAnalyzerResult();
            }

            var stopwordRemoverResult = StopwordRemover.RemoveStopwords(textSplitResult.Words);

            var words = stopwordRemoverResult.Words;
            var distinctWords = words.Distinct().ToList();

            var numberOfWords = words.Count;
            var numberOfUniqueWords = distinctWords.Count;
            var averageWordLength = words.Any() ? words.Average(s => s.Length) : 0.0;
            var numberOfchapters = text.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None).Count();

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
}
