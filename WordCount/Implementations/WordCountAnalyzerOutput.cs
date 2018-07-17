using WordCount.Interfaces;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;

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

            _statisticsOutput
                .WriteOutputs(lengthOfMaxString: 20)
                    .WriteNumberOfWords(numberOfWords: wordCountAnalyzerResult.NumberOfWords)
                    .WriteNumberOfUniqeWords(numberOfUniqeWords: wordCountAnalyzerResult.NumberOfUniqueWords)
                    .WriteAverageWordLength(averageWordLength: wordCountAnalyzerResult.AverageWordLength)
                    .WriteNumberOfChapters(numberOfChapters: wordCountAnalyzerResult.NumberOfChapters);
        }
    }
}
