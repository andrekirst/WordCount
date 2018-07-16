using WordCount.Interfaces;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;
using CultureInfo = System.Globalization.CultureInfo;

namespace WordCount.Implementations
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly IStatisticsOutput _statisticsOutput;
        private readonly ILanguageResource _languageResource;

        public WordCountAnalyzerOutput(
            IDisplayOutput displayOutput,
            IStatisticsOutput statisticsOutput,
            ILanguageResource languageResource)
        {
            _displayOutput = displayOutput;
            _statisticsOutput = statisticsOutput;
            _languageResource = languageResource;
        }

        public void DisplayResult(WordCountAnalyzerResult wordCountAnalyzerResult)
        {
            _displayOutput.WriteResourceLine(resourceIdent: "STATISTICS");

            int lengthOfTheLongestStringOfRessourceStrings = _languageResource.DetectLongestResourceString(
                resourceIdents: new[] { "NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS" });

            _statisticsOutput.WriteNumberOfWords(
                numberOfWords: wordCountAnalyzerResult.NumberOfWords,
                maxCountOfFillingPoints: lengthOfTheLongestStringOfRessourceStrings);

            _statisticsOutput.WriteNumberOfUniqeWords(
                numberOfUniqeWords: wordCountAnalyzerResult.NumberOfUniqueWords,
                maxCountOfFillingPoints: lengthOfTheLongestStringOfRessourceStrings);

            _statisticsOutput.WriteAveragewordLength(
                averageWordLength: wordCountAnalyzerResult.AverageWordLength,
                maxCountOfFillingPoints: lengthOfTheLongestStringOfRessourceStrings);

            _statisticsOutput.WriteNumberOfChapters(
                numberOfChapters: wordCountAnalyzerResult.NumberOfChapters,
                maxCountOfFillingPoints: lengthOfTheLongestStringOfRessourceStrings);
        }
    }
}
