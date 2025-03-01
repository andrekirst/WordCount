using WordCount.Extensions;

namespace WordCount;

public interface IStatisticsOutput
{
    void WriteNumberOfWords(
        int numberOfWords,
        int maxCountOfFillingPoints);

    void WriteNumberOfUniqeWords(
        int numberOfUniqeWords,
        int maxCountOfFillingPoints);

    void WriteAverageWordLength(
        double averageWordLength,
        int maxCountOfFillingPoints);

    void WriteNumberOfChapters(
        int numberOfChapters,
        int maxCountOfFillingPoints);
}

public class StatisticsOutput(
    IDisplayOutput displayOutput,
    ILanguageResource languageResource,
    ILanguageDecision languageDecision) : IStatisticsOutput
{
    public void WriteNumberOfWords(int numberOfWords, int maxCountOfFillingPoints)
    {
        var resourceValue = languageResource.GetResourceStringById("NUMBER_OF_WORDS");
        var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

        output = $"- {output} {numberOfWords}";
        displayOutput.WriteLine(output);
    }

    public void WriteNumberOfUniqeWords(int numberOfUniqeWords, int maxCountOfFillingPoints)
    {
        var resourceValue = languageResource.GetResourceStringById("UNIQUE");
        var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

        output = $"- {output} {numberOfUniqeWords}";
        displayOutput.WriteLine(output);
    }

    public void WriteAverageWordLength(double averageWordLength, int maxCountOfFillingPoints)
    {
        var currentCulture = languageDecision.DecideLanguage().Culture;

        var resourceValueAverageWordLength = languageResource.GetResourceStringById("AVERAGE_WORD_LENGTH");
        var resourceValueCharacters = languageResource.GetResourceStringById("CHARACTERS");
        var output = resourceValueAverageWordLength.FillRightWithPoints(maxCountOfFillingPoints);

        var averageWordLengthAsString = averageWordLength.ToString("N2", currentCulture);

        output = $"- {output} {averageWordLengthAsString} {resourceValueCharacters}";
        displayOutput.WriteLine(output);
    }

    public void WriteNumberOfChapters(int numberOfChapters, int maxCountOfFillingPoints)
    {
        var resourceValue = languageResource.GetResourceStringById("CHAPTERS");
        var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

        output = $"- {output} {numberOfChapters}";
        displayOutput.WriteLine(output);
    }
}
