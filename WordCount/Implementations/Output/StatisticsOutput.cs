using System.Globalization;
using WordCount.Interfaces.Language;
using WordCount.Interfaces.Output;
using WordCount.Extensions;

namespace WordCount.Implementations.Output
{
    public class StatisticsOutput : IStatisticsOutput
    {
        private readonly IDisplayOutput _displayOutput;
        private readonly ILanguageResource _languageResource;
        private readonly ILanguageDecision _languageDecision;

        public StatisticsOutput(
            IDisplayOutput displayOutput,
            ILanguageResource languageResource,
            ILanguageDecision languageDecision)
        {
            _displayOutput = displayOutput;
            _languageResource = languageResource;
            _languageDecision = languageDecision;
        }

        public void WriteNumberOfWords(int numberOfWords, int maxCountOfFillingPoints)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: "NUMBER_OF_WORDS");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfWords}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfUniqeWords(int numberOfUniqeWords, int maxCountOfFillingPoints)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: "UNIQUE");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfUniqeWords}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteAverageWordLength(double averageWordLength, int maxCountOfFillingPoints)
        {
            CultureInfo currentCulture = _languageDecision.DecideLanguage().Culture;

            string resourceValueAverageWordLength = _languageResource.GetResourceStringById(resourceIdent: "AVERAGE_WORD_LENGTH");
            string resourceValueCharacters = _languageResource.GetResourceStringById(resourceIdent: "CHARACTERS");
            string output = resourceValueAverageWordLength.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            string averageWordLengthAsString = averageWordLength.ToString(format: "N2", provider: currentCulture);

            output = $"- {output} {averageWordLengthAsString} {resourceValueCharacters}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfChapters(int numberOfChapters, int maxCountOfFillingPoints)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: "CHAPTERS");
            string output = resourceValue.FillRightWithPoints(totalWidth: maxCountOfFillingPoints);

            output = $"- {output} {numberOfChapters}";
            _displayOutput.WriteLine(text: output);
        }
    }
}
