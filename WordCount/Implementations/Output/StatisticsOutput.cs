using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Extensions;

namespace WordCount.Implementations.Output
{
    public class StatisticsOutput : IStatisticsOutput
    {
        private IDisplayOutput DisplayOutput { get; }
        private ILanguageResource LanguageResource { get; }
        private ILanguageDecision LanguageDecision { get; }

        public StatisticsOutput(
            IDisplayOutput displayOutput,
            ILanguageResource languageResource,
            ILanguageDecision languageDecision)
        {
            DisplayOutput = displayOutput;
            LanguageResource = languageResource;
            LanguageDecision = languageDecision;
        }

        public void WriteNumberOfWords(int numberOfWords, int maxCountOfFillingPoints)
        {
            var resourceValue = LanguageResource.GetResourceStringById("NUMBER_OF_WORDS");
            var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

            output = $"- {output} {numberOfWords}";
            DisplayOutput.WriteLine(output);
        }

        public void WriteNumberOfUniqeWords(int numberOfUniqeWords, int maxCountOfFillingPoints)
        {
            var resourceValue = LanguageResource.GetResourceStringById("UNIQUE");
            var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

            output = $"- {output} {numberOfUniqeWords}";
            DisplayOutput.WriteLine(output);
        }

        public void WriteAverageWordLength(double averageWordLength, int maxCountOfFillingPoints)
        {
            var currentCulture = LanguageDecision.DecideLanguage().Culture;

            var resourceValueAverageWordLength = LanguageResource.GetResourceStringById("AVERAGE_WORD_LENGTH");
            var resourceValueCharacters = LanguageResource.GetResourceStringById("CHARACTERS");
            var output = resourceValueAverageWordLength.FillRightWithPoints(maxCountOfFillingPoints);

            var averageWordLengthAsString = averageWordLength.ToString("N2", currentCulture);

            output = $"- {output} {averageWordLengthAsString} {resourceValueCharacters}";
            DisplayOutput.WriteLine(output);
        }

        public void WriteNumberOfChapters(int numberOfChapters, int maxCountOfFillingPoints)
        {
            var resourceValue = LanguageResource.GetResourceStringById("CHAPTERS");
            var output = resourceValue.FillRightWithPoints(maxCountOfFillingPoints);

            output = $"- {output} {numberOfChapters}";
            DisplayOutput.WriteLine(output);
        }
    }
}
