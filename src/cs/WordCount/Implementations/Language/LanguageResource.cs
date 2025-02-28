using System.Linq;
using WordCount.Abstractions.SystemAbstractions.Globalization;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Helpers;
using WordCount.Interfaces.Language;

namespace WordCount.Implementations.Language;

public class LanguageResource(
    ILanguageDecision languageDecision,
    ICultureInfo cultureInfo,
    IResourceManager resourceManager) : ILanguageResource
{
    public string GetResourceStringById(string resourceIdent)
    {
        var decidedLanguage = languageDecision.DecideLanguage();

        var mappedLanguageCulture = LanguageToCultureMapping.Mappings[decidedLanguage.Language];

        var currentCultureInfo = cultureInfo.GetCultureInfo(mappedLanguageCulture);

        return resourceManager.GetString(resourceIdent, currentCultureInfo);
    }

    public int DetectLongestResourceString(string[] resourceIdents) => resourceIdents.Max(s => GetResourceStringById(s).Length);
}