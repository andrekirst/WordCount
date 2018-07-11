using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Results;
using CultureInfo = System.Globalization.CultureInfo;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly ILanguageParameterParser _languageParameterParser;

        public WordCountAnalyzerOutput(
            IDisplayOutput displayOutput,
            ILanguageParameterParser languageParameterParser)
        {
            _displayOutput = displayOutput;
            _languageParameterParser = languageParameterParser;
        }

        public void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            CultureInfo culture = _languageParameterParser.ParseLanguageParameter().Culture;

            int numberOfWords = wordCountAnalyzerResult.NumberOfWords;
            int numberOfUniqueWords = wordCountAnalyzerResult.NumberOfUniqueWords;
            string averageWordLengthAsString = wordCountAnalyzerResult.AverageWordLength.ToString(format: "N2", provider: culture);
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
        }
    }
}
