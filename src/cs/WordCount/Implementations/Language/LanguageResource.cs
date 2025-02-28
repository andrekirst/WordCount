using System.Linq;
using WordCount.Abstractions.SystemAbstractions.Globalization;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Helpers;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language
{
    public class LanguageResource : ILanguageResource
    {
        private ILanguageDecision LanguageDecision { get; }
        private ICultureInfo CultureInfo { get; }
        private IResourceManager ResourceManager { get; }

        public LanguageResource(
            ILanguageDecision languageDecision,
            ICultureInfo cultureInfo,
            IResourceManager resourceManager)
        {
            LanguageDecision = languageDecision;
            CultureInfo = cultureInfo;
            ResourceManager = resourceManager;
        }

        public string GetResourceStringById(string resourceIdent)
        {
            var languageDecision = LanguageDecision.DecideLanguage();

            var mappedLanguageCulture = LanguageToCultureMapping.Mappings[languageDecision.Language];

            var currentCultureInfo = CultureInfo.GetCultureInfo(mappedLanguageCulture);

            return ResourceManager.GetString(
                resourceIdent,
                currentCultureInfo);
        }

        public int DetectLongestResourceString(string[] resourceIdents) => resourceIdents.Max(s => GetResourceStringById(s).Length);
    }
}