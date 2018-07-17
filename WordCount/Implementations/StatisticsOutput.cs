using System.Globalization;
using WordCount.Interfaces;
using WordCount.Interfaces.Language;

namespace WordCount.Implementations
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
            string output = resourceValue.PadRight(
                totalWidth: maxCountOfFillingPoints,
                paddingChar: '.');

            output = $"- {output} {numberOfWords}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfUniqeWords(int numberOfUniqeWords, int maxCountOfFillingPoints)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: "UNIQUE");
            string output = resourceValue.PadRight(
                totalWidth: maxCountOfFillingPoints,
                paddingChar: '.');

            output = $"- {output} {numberOfUniqeWords}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteAveragewordLength(double averageWordLength, int maxCountOfFillingPoints)
        {
            CultureInfo currentCulture = _languageDecision.DecideLanguage().Culture;

            string resourceValueAverageWordLength = _languageResource.GetResourceStringById(resourceIdent: "AVERAGE_WORD_LENGTH");
            string resourceValueCharacters = _languageResource.GetResourceStringById(resourceIdent: "CHARACTERS");
            string output = resourceValueAverageWordLength.PadRight(
                totalWidth: maxCountOfFillingPoints,
                paddingChar: '.');

            string averageWordLengthAsString = averageWordLength.ToString(format: "N2", provider: currentCulture);

            output = $"- {output} {averageWordLengthAsString} {resourceValueCharacters}";
            _displayOutput.WriteLine(text: output);
        }

        public void WriteNumberOfChapters(int numberOfChapters, int maxCountOfFillingPoints)
        {
            string resourceValue = _languageResource.GetResourceStringById(resourceIdent: "CHAPTERS");
            string output = resourceValue.PadRight(
                totalWidth: maxCountOfFillingPoints,
                paddingChar: '.');

            output = $"- {output} {numberOfChapters}";
            _displayOutput.WriteLine(text: output);
        }
    }
}
