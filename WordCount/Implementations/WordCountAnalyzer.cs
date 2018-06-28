using System.Linq;
using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        private readonly ITextSplit _textSplit;
        private readonly IStopwordRemover _stopwordRemover;

        public WordCountAnalyzer(
            ITextSplit textSplit,
            IStopwordRemover stopwordRemover)
        {
            _textSplit = textSplit;
            _stopwordRemover = stopwordRemover;
        }

        public WordCountAnalyzerResult Analyze(string text)
        {
            TextSplitResult textSplitResult = _textSplit.Split(text: text);

            if (!textSplitResult.WordsAvailable)
            {
                return new WordCountAnalyzerResult();
            }

            StopwordRemoverResult stopwordRemoverResult = _stopwordRemover.RemoveStopwords(values: textSplitResult.Words);

            List<string> words = stopwordRemoverResult.Values;
            List<string> distinctWords = words
                .Distinct()
                .ToList();

            int numberOfWords = words.Count;
            int numberOfUniqueWords = distinctWords.Count;
            double averageWordLength = words.Average(selector: s => s.Length);

            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords,
                NumberOfUniqueWords = numberOfUniqueWords,
                AverageWordLength = averageWordLength,
                DistinctWords = distinctWords
            };
        }
    }
}
