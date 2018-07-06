using System.Globalization;
using WordCount.Interfaces;
using WordCount.Models.Results;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private readonly IDisplayOutput _displayOutput;

        public WordCountAnalyzerOutput(IDisplayOutput displayOutput)
        {
            _displayOutput = displayOutput;
        }

        public void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            int numberOfWords = wordCountAnalyzerResult.NumberOfWords;
            int numberOfUniqueWords = wordCountAnalyzerResult.NumberOfUniqueWords;
            string averageWordLengthAsString = wordCountAnalyzerResult.AverageWordLength.ToString("N2", CultureInfo.GetCultureInfo("en-US"));
            int numberOfChapters = wordCountAnalyzerResult.NumberOfChapters;

            string result = 
                $"Number of words: {numberOfWords}"
              + $", unique: {numberOfUniqueWords}"
              + $"; average word length: {averageWordLengthAsString} characters"
              + $"; chapters: {numberOfChapters}";

            _displayOutput.WriteLine(text: result);
        }
    }
}
