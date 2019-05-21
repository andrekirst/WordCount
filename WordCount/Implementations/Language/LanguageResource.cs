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
            DecideLanguageResult languageDecision = LanguageDecision.DecideLanguage();

            string mappedLanguageCulture = LanguageToCultureMapping.Mappings[key: languageDecision.Language];

            System.Globalization.CultureInfo currentCultureInfo = CultureInfo.GetCultureInfo(culture: mappedLanguageCulture);

            return ResourceManager.GetString(
                name: resourceIdent,
                cultureInfo: currentCultureInfo);
        }

        public int DetectLongestResourceString(string[] resourceIdents) => resourceIdents.Max(selector: s => GetResourceStringById(resourceIdent: s).Length);
    }
}