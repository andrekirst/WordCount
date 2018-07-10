using WordCount.Abstractions.CultureInfo;
using WordCount.Abstractions.ResourceManager;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations
{
    public class LanguageResource : ILanguageResource
    {
        private readonly ILanguageParameterParser _languageParameterParser;
        private readonly ICultureInfo _cultureInfo;
        private readonly IResourceManager _resourceManager;

        public LanguageResource(
            ILanguageParameterParser languageParameterParser,
            ICultureInfo cultureInfo,
            IResourceManager resourceManager)
        {
            _languageParameterParser = languageParameterParser;
            _cultureInfo = cultureInfo;
            _resourceManager = resourceManager;
        }

        public string GetResourceStringById(string resourceIdent)
        {
            LanguageParameter languageParameter = _languageParameterParser.ParseLanguageParameter();

            string language =
                languageParameter.IsPresent ? languageParameter.Language : "en";

            string mappedLanguageCulture = LanguageCultureMappings.Mappings[key: language];

            System.Globalization.CultureInfo currentCultureInfo = _cultureInfo.GetCultureInfo(culture: mappedLanguageCulture);

            return _resourceManager.GetString(
                name: resourceIdent,
                cultureInfo: currentCultureInfo);
        }
    }
}