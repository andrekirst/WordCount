namespace WordCount;

public interface IWordCountAnalyzerOutput
{
    void DisplayResult(WordCountAnalyzerResult result);
}

public class WordCountAnalyzerOutput(
    IDisplayOutput displayOutput,
    IStatisticsOutput statisticsOutput,
    ILanguageResource languageResource) : IWordCountAnalyzerOutput
{
    public void DisplayResult(WordCountAnalyzerResult result)
    {
        displayOutput.WriteResourceLine("STATISTICS");

        var lengthOfMaxString = languageResource.DetectLongestResourceString(
            ["NUMBER_OF_WORDS", "UNIQUE", "AVERAGE_WORD_LENGTH", "CHAPTERS"]);

        statisticsOutput.WriteNumberOfWords(result.NumberOfWords, lengthOfMaxString);
        statisticsOutput.WriteNumberOfUniqeWords(result.NumberOfUniqueWords, lengthOfMaxString);
        statisticsOutput.WriteAverageWordLength(result.AverageWordLength, lengthOfMaxString);
        statisticsOutput.WriteNumberOfChapters(result.NumberOfChapters, lengthOfMaxString);
    }
}
