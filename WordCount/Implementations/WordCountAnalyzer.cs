using System;
using System.Linq;
using System.Collections.Generic;
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
            TextSplitResult textSplitResult = TextSplit.Split(text: text);

            if (!textSplitResult.WordsAvailable)
            {
                return new WordCountAnalyzerResult();
            }

            StopwordRemoverResult stopwordRemoverResult = StopwordRemover.RemoveStopwords(words: textSplitResult.Words);

            List<string> words = stopwordRemoverResult.Words;
            List<string> distinctWords = words.Distinct().ToList();

            int numberOfWords = words.Count;
            int numberOfUniqueWords = distinctWords.Count;
            double averageWordLength = words.Any() ? words.Average(selector: s => s.Length) : 0.0;
            int numberOfchapters = text.Split(separator: new[] {Environment.NewLine + Environment.NewLine}, options: StringSplitOptions.None).Count();

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
