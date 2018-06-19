using System.Collections.Generic;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        private readonly ITextSplit _textSplit;

        public WordCountAnalyzer(ITextSplit textSplit)
        {
            _textSplit = textSplit;
        }

        public WordCountAnalyzerResult Analyze(
            string text,
            List<string> stopwords = null)
        {
            TextSplitResult textSplitResult = _textSplit.Split(text: text);

            if (!textSplitResult.ValuesAvailable)
            {
                return new WordCountAnalyzerResult()
                {
                    NumberOfWords = 0
                };
            }

            if (stopwords != null)
            {
                textSplitResult.Values.RemoveAll(match: stopwords.Contains); 
            }

            int numberOfWords = textSplitResult.Values.Count;
            return new WordCountAnalyzerResult()
            {
                NumberOfWords = numberOfWords
            };
        }
    }
}
