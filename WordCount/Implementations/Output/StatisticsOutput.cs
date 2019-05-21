using System.Globalization;
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
            string resourceValue = LanguageResource.GetResourceStringById(resourceIdent: "NUMBER_OF_WORDS");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfWords}";
            DisplayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfUniqeWords(int numberOfUniqeWords, int maxCountOfFillingPoints)
        {
            string resourceValue = LanguageResource.GetResourceStringById(resourceIdent: "UNIQUE");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfUniqeWords}";
            DisplayOutput.WriteLine(text: output);
        }

        public void WriteAverageWordLength(double averageWordLength, int maxCountOfFillingPoints)
        {
            CultureInfo currentCulture = LanguageDecision.DecideLanguage().Culture;

            string resourceValueAverageWordLength = LanguageResource.GetResourceStringById(resourceIdent: "AVERAGE_WORD_LENGTH");
            string resourceValueCharacters = LanguageResource.GetResourceStringById(resourceIdent: "CHARACTERS");
            string output = resourceValueAverageWordLength.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            string averageWordLengthAsString = averageWordLength.ToString(format: "N2", provider: currentCulture);

            output = $"- {output} {averageWordLengthAsString} {resourceValueCharacters}";
            DisplayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfChapters(int numberOfChapters, int maxCountOfFillingPoints)
        {
            string resourceValue = LanguageResource.GetResourceStringById(resourceIdent: "CHAPTERS");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfChapters}";
            DisplayOutput.WriteLine(text: output);
        }
    }
}
