using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations.Output
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

        public void DisplayResult(WordCountAnalyzerResult result)
        {
            _displayOutput.WriteResourceLine(resourceIdent: "STATISTICS");

            int lengthOfMaxString = _languageResource.DetectLongestResourceString(
                resourceIdents: new[] { "NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS" });

            _statisticsOutput.WriteNumberOfWords(
                numberOfWords: result.NumberOfWords,
                maxCountOfFillingPoints: lengthOfMaxString);

            _statisticsOutput.WriteNumberOfUniqeWords(
                numberOfUniqeWords: result.NumberOfUniqueWords,
                maxCountOfFillingPoints: lengthOfMaxString);

            _statisticsOutput.WriteAverageWordLength(
                averageWordLength: result.AverageWordLength,
                maxCountOfFillingPoints: lengthOfMaxString);

            _statisticsOutput.WriteNumberOfChapters(
                numberOfChapters: result.NumberOfChapters,
                maxCountOfFillingPoints: lengthOfMaxString);
        }
    }
}
