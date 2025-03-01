using System;
using System.Globalization;
using Microsoft.Extensions.Options;
using WordCount.Extensions;
using WordCount.Helpers;

namespace WordCount;

public interface ILanguageDecision
{
    DecideLanguageResult DecideLanguage();
}

public class LanguageDecision(
    IOptions<WordCountOptions> options,
    IOptions<WordCountCommand.Settings> settings) : ILanguageDecision
{
    private readonly WordCountCommand.Settings _settings = settings.Value;
    private readonly WordCountOptions _options = options.Value;
    private const string DefaultFallbackLanguage = Languages.English;
    private DecideLanguageResult? _cache;

    public DecideLanguageResult DecideLanguage()
    {
        if (_cache != null)
        {
            return _cache;
        }

        var language = string.IsNullOrEmpty(_settings.Language) ? _options.DefaultLanguage : _settings.Language;

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
    public string Language { get; set; } = default!;

    public CultureInfo Culture { get; set; } = default!;
}
