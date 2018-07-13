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

            _displayOutput.WriteResource(
                resourceIdent: "NUMBER_OF_WORDS",
                placeholderValues: numberOfWords);
            _displayOutput.Write(text: ", ");

            _displayOutput.WriteResource(
                resourceIdent: "UNIQUE",
                placeholderValues: numberOfUniqueWords);
            _displayOutput.Write(text: ", ");

            _displayOutput
                .WriteResource(
                    resourceIdent: "AVERAGE_WORD_LENGTH",
                    placeholderValues: averageWordLengthAsString);
            _displayOutput.Write(text: ", ");

            _displayOutput
                .WriteResource(
                    resourceIdent: "CHAPTERS",
                    placeholderValues: numberOfChapters);

            _displayOutput.WriteLine();
        }
    }
}
