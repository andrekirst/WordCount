using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Helpers;
using WordCount.Interfaces;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Interfaces.Language;
using WordCount.Models.Parameters;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language
{
    public class LanguageDecision : ILanguageDecision
    {
        private const string DefaultFallbackLanguage = "en";
        private readonly IAppSettingsReader _appSettingsReader;
        private readonly ILanguageParameterParser _languageParameterParser;
        private readonly IConsole _console;
        private DecideLanguageResult _cache;

        public LanguageDecision(
            IAppSettingsReader appSettingsReader,
            ILanguageParameterParser languageParameterParser,
            IConsole console)
        {
            _appSettingsReader = appSettingsReader;
            _languageParameterParser = languageParameterParser;
            _console = console;
        }

        public DecideLanguageResult DecideLanguage()
        {
            if (_cache != null)
            {
                return _cache;
            }

            string language = _appSettingsReader.DefaultLanguage;

            LanguageParameter languageParameter = _languageParameterParser.ParseLanguageParameter();

            if (languageParameter.IsPresent)
            {
                language = languageParameter.Language;
            }

            if (language.IsFilled() &&
                !LanguageToCultureMapping.Mappings.ContainsKey(key: language))
            {
                _console.WriteLine(text: $"Language \"{language}\" not supported.");
                language = DefaultFallbackLanguage;
            }

            language = language.IsFilled() ? language : DefaultFallbackLanguage;
            return _cache = new DecideLanguageResult
            {
                Language = language
            };
        }
    }
}
