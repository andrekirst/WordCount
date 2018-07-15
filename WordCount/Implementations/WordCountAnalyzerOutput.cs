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

            _displayOutput.WriteResourceLine(resourceIdent: "STATISTICS");

            _displayOutput.WriteResourceLine(
                resourceIdent: "NUMBER_OF_WORDS",
                placeholderValues: wordCountAnalyzerResult.NumberOfWords);

            _displayOutput.WriteResourceLine(
                resourceIdent: "UNIQUE",
                placeholderValues: wordCountAnalyzerResult.NumberOfUniqueWords);

            _displayOutput.WriteResourceLine(
                    resourceIdent: "AVERAGE_WORD_LENGTH",
                    placeholderValues: wordCountAnalyzerResult.AverageWordLength.ToString(format: "N2", provider: culture));

            _displayOutput
                .WriteResourceLine(
                    resourceIdent: "CHAPTERS",
                    placeholderValues: wordCountAnalyzerResult.NumberOfChapters);
        }
    }
}
