using System.Linq;
using System.Resources;
using WordCount.Helpers;

namespace WordCount;

public interface ILanguageResource
{
    string GetResourceStringById(string resourceIdent);

    int DetectLongestResourceString(string[] resourceIdents);
}

public class LanguageResource(
    ILanguageDecision languageDecision,
    ResourceManager resourceManager) : ILanguageResource
{
    public string GetResourceStringById(string resourceIdent)
    {
        var decidedLanguage = languageDecision.DecideLanguage();

        var mappedLanguageCulture = LanguageToCultureMapping.Mappings[decidedLanguage.Language];

        return resourceManager.GetString(resourceIdent, mappedLanguageCulture) ?? string.Empty;
    }

    public int DetectLongestResourceString(string[] resourceIdents) => resourceIdents.Max(s => GetResourceStringById(s).Length);
}