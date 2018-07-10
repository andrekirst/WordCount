using WordCount.Interfaces;
using WordCount.Models.Results;
using CultureInfo = System.Globalization.CultureInfo;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private readonly IDisplayOutput _displayOutput;

        public WordCountAnalyzerOutput(
            IDisplayOutput displayOutput)
        {
            _displayOutput = displayOutput;
        }

        public void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            int numberOfWords = wordCountAnalyzerResult.NumberOfWords;
            int numberOfUniqueWords = wordCountAnalyzerResult.NumberOfUniqueWords;
            string averageWordLengthAsString = wordCountAnalyzerResult.AverageWordLength.ToString(format: "N2", provider: CultureInfo.GetCultureInfo("en-US"));
            int numberOfChapters = wordCountAnalyzerResult.NumberOfChapters;

            _displayOutput.WriteResourceStringWithValues(
                resourceIdent: "NUMBER_OF_WORDS",
                values: numberOfWords);
            _displayOutput.Write(text: ", ");

            _displayOutput.WriteResourceStringWithValues(
                resourceIdent: "UNIQUE",
                values: numberOfUniqueWords);
            _displayOutput.Write(text: ", ");

            _displayOutput
                .WriteResourceStringWithValues(
                    resourceIdent: "AVERAGE_WORD_LENGTH",
                    values: averageWordLengthAsString);
            _displayOutput.Write(text: ", ");

            _displayOutput
                .WriteResourceStringWithValues(
                    resourceIdent: "CHAPTERS",
                    values: numberOfChapters);

            _displayOutput.WriteLine();

            //string result =
            //    $"Number of words: {numberOfWords}"
            //  + $", unique: {numberOfUniqueWords}"
            //  + $"; average word length: {averageWordLengthAsString} characters"
            //  + $"; chapters: {numberOfChapters}";

            //_displayOutput.WriteLine(text: result);
        }
    }
}
