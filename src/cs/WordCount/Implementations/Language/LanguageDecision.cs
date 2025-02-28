using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language;

public class LanguageDecision(
    IAppSettingsReader appSettingsReader,
    ILanguageParameterParser languageParameterParser,
    IConsole console) : ILanguageDecision
{
    private const string DefaultFallbackLanguage = "en";
    private DecideLanguageResult _cache;

    public DecideLanguageResult DecideLanguage()
    {
        if (_cache != null)
        {
            return _cache;
        }

        var language = appSettingsReader.DefaultLanguage;

        var languageParameter = languageParameterParser.ParseLanguageParameter();

        if (languageParameter.IsPresent)
        {
            language = languageParameter.Language;
        }

        if (language.IsFilled() &&
            !LanguageToCultureMapping.Mappings.ContainsKey(language))
        {
            console.WriteLine($"Language \"{language}\" not supported.");
            language = DefaultFallbackLanguage;
        }

        language = language.IsFilled() ? language : DefaultFallbackLanguage;
        
        return _cache = new DecideLanguageResult
        {
            Language = language
        };
    }
}
