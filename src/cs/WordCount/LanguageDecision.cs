using System;
using System.Globalization;
using Microsoft.Extensions.Options;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Models.Parameters;

namespace WordCount;

public interface ILanguageDecision
{
    DecideLanguageResult DecideLanguage();
}

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

public class DecideLanguageResult
{
    public string Language { get; set; }

    public CultureInfo Culture { get; set; }
}
