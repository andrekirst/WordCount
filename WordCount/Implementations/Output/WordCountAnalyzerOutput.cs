using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Models.Results;

namespace WordCount.Implementations.Output
{
    public class WordCountAnalyzerOutput : IWordCountAnalyzerOutput
    {
        private IDisplayOutput DisplayOutput { get; }
        private IStatisticsOutput StatisticsOutput { get; }
        private ILanguageResource LanguageResource { get; }

        public WordCountAnalyzerOutput(
            IDisplayOutput displayOutput,
            IStatisticsOutput statisticsOutput,
            ILanguageResource languageResource)
        {
            DisplayOutput = displayOutput;
            StatisticsOutput = statisticsOutput;
            LanguageResource = languageResource;
        }

        public void DisplayResult(WordCountAnalyzerResult result)
        {
            DisplayOutput.WriteResourceLine(resourceIdent: "STATISTICS");

            int lengthOfMaxString = LanguageResource.DetectLongestResourceString(
                resourceIdents: new[] { "NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS" });

            StatisticsOutput.WriteNumberOfWords(
                numberOfWords: result.NumberOfWords,
                maxCountOfFillingPoints: lengthOfMaxString);

            StatisticsOutput.WriteNumberOfUniqeWords(
                numberOfUniqeWords: result.NumberOfUniqueWords,
                maxCountOfFillingPoints: lengthOfMaxString);

            StatisticsOutput.WriteAverageWordLength(
                averageWordLength: result.AverageWordLength,
                maxCountOfFillingPoints: lengthOfMaxString);

            StatisticsOutput.WriteNumberOfChapters(
                numberOfChapters: result.NumberOfChapters,
                maxCountOfFillingPoints: lengthOfMaxString);
        }
    }
}
