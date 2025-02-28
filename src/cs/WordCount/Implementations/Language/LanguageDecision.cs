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
        private IAppSettingsReader AppSettingsReader { get; }
        private ILanguageParameterParser LanguageParameterParser { get; }
        private IConsole Console { get; }
        private const string DefaultFallbackLanguage = "en";
        private DecideLanguageResult _cache;

        public LanguageDecision(
            IAppSettingsReader appSettingsReader,
            ILanguageParameterParser languageParameterParser,
            IConsole console)
        {
            AppSettingsReader = appSettingsReader;
            LanguageParameterParser = languageParameterParser;
            Console = console;
        }

        public DecideLanguageResult DecideLanguage()
        {
            if (_cache != null)
            {
                return _cache;
            }

            var language = AppSettingsReader.DefaultLanguage;

            var languageParameter = LanguageParameterParser.ParseLanguageParameter();

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
}
