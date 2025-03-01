using System;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Language;
using WordCount.Models.Parameters;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language;

public class LanguageDecision(
    IAppSettingsReader appSettingsReader,
    IParameterParser<LanguageParameter> languageParameterParser) : ILanguageDecision
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
        var args = Environment.GetCommandLineArgs();
        var languageParameter = languageParameterParser.ParseParameter(args);

        if (languageParameter.IsPresent)
        {
            language = languageParameter.Language;
        }

        if (language.IsFilled() &&
            !LanguageToCultureMapping.Mappings.ContainsKey(language))
        {
            Console.WriteLine($"Language \"{language}\" not supported.");
            language = DefaultFallbackLanguage;
        }

        language = language.IsFilled() ? language : DefaultFallbackLanguage;
        
        return _cache = new DecideLanguageResult
        {
            Language = language
        };
    }
}
