using System.Linq;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Helpers;
using WordCount.Interfaces.Language;

namespace WordCount.Implementations.Language;

public class LanguageResource(
    ILanguageDecision languageDecision,
    IResourceManager resourceManager) : ILanguageResource
{
    public string GetResourceStringById(string resourceIdent)
    {
        var decidedLanguage = languageDecision.DecideLanguage();

        var mappedLanguageCulture = LanguageToCultureMapping.Mappings[decidedLanguage.Language];

        return resourceManager.GetString(resourceIdent, mappedLanguageCulture);
    }

    public int DetectLongestResourceString(string[] resourceIdents) => resourceIdents.Max(s => GetResourceStringById(s).Length);
}