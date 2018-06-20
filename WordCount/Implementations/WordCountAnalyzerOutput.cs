using System.Globalization;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        public string DisplayResultAsString(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            int numberOfWords = wordCountAnalyzerResult.NumberOfWords;
            int numberOfUniqueWords = wordCountAnalyzerResult.NumberOfUniqueWords;
            string averageWordLengthAsString = wordCountAnalyzerResult.AverageWordLength.ToString("N2", CultureInfo.GetCultureInfo("en-US"));
            return $"Number of words: {numberOfWords}"
                 + $", unique: {numberOfUniqueWords}"
                 + $"; average word length: {averageWordLengthAsString} characters";
        }
    }
}
