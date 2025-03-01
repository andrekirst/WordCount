using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Language;
using WordCount.Models.Parameters;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language;

public class LanguageDecision(
    IOptions<WordCountOptions> options,
    IParameterParser<LanguageParameter> languageParameterParser) : ILanguageDecision
{
    private readonly WordCountOptions _options = options.Value;
    private const string DefaultFallbackLanguage = Languages.English;
    private DecideLanguageResult _cache;

    public DecideLanguageResult DecideLanguage()
    {
        if (_cache != null)
        {
            return _cache;
        }

        var language = _options.DefaultLanguage;
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
