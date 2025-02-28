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
            DisplayOutput.WriteResourceLine("STATISTICS");

            var lengthOfMaxString = LanguageResource.DetectLongestResourceString(
                new[] { "NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS" });

            StatisticsOutput.WriteNumberOfWords(
                result.NumberOfWords,
                lengthOfMaxString);

            StatisticsOutput.WriteNumberOfUniqeWords(
                result.NumberOfUniqueWords,
                lengthOfMaxString);

            StatisticsOutput.WriteAverageWordLength(
                result.AverageWordLength,
                lengthOfMaxString);

            StatisticsOutput.WriteNumberOfChapters(
                result.NumberOfChapters,
                lengthOfMaxString);
        }
    }
}
