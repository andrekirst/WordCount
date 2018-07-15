using WordCount.Interfaces;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;
using CultureInfo = System.Globalization.CultureInfo;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly ILanguageDecision _languageDecison;

        public WordCountAnalyzerOutput(
            IDisplayOutput displayOutput,
            ILanguageDecision languageDecison)
        {
            _displayOutput = displayOutput;
            _languageDecison = languageDecison;
        }

        public void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            CultureInfo culture = _languageDecison.DecideLanguage().Culture;

            int numberOfWords = wordCountAnalyzerResult.NumberOfWords;
            int numberOfUniqueWords = wordCountAnalyzerResult.NumberOfUniqueWords;
            string averageWordLengthAsString = wordCountAnalyzerResult.AverageWordLength.ToString(format: "N2", provider: culture);
            int numberOfChapters = wordCountAnalyzerResult.NumberOfChapters;

            _displayOutput.WriteResourceLine(resourceIdent: "STATISTICS");

            _displayOutput.WriteResourceLine(
                resourceIdent: "NUMBER_OF_WORDS",
                placeholderValues: numberOfWords);

            _displayOutput.WriteResourceLine(
                resourceIdent: "UNIQUE",
                placeholderValues: numberOfUniqueWords);

            _displayOutput.WriteResourceLine(
                    resourceIdent: "AVERAGE_WORD_LENGTH",
                    placeholderValues: averageWordLengthAsString);

            _displayOutput
                .WriteResourceLine(
                    resourceIdent: "CHAPTERS",
                    placeholderValues: numberOfChapters);
        }
    }
}
