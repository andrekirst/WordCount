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

            if (!textSplitResult.ValuesAvailable)
            {
                return new WordCountAnalyzerResult();
            }

            StopwordRemoverResult stopwordRemoverResult = _stopwordRemover.RemoveStopwords(values: textSplitResult.Values);

            List<string> words = stopwordRemoverResult.Values;

            int numberOfWords = words.Count;
            int numberOfUniqueWords = words.Distinct().Count();
            double averageWordLength = words.Average(s => s.Length);
            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords,
                NumberOfUniqueWords = numberOfUniqueWords,
                AverageWordLength = averageWordLength
            };
        }
    }
}
