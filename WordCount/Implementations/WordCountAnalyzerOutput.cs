using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        public string DisplayResultAsString(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            return $"Number of words: {wordCountAnalyzerResult.NumberOfWords}";
        }
    }
}
