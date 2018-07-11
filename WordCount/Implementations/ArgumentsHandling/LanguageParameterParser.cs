using System;
using System.Linq;
using WordCount.Abstractions.Console;
using WordCount.Extensions;
using WordCount.Abstractions.Environment;
using WordCount.Helpers;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class LanguageParameterParser : BaseParameterParser<LanguageParameter>, ILanguageParameterParser
    {
        private readonly IEnvironment _environment;
        private readonly IConsole _console;

        public LanguageParameterParser(
            IEnvironment environment,
            IConsole console)
        {
            _environment = environment;
            _console = console;
        }

        public LanguageParameter ParseLanguageParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] commandLineArgs = _environment.GetCommandLineArgs() ?? Array.Empty<string>();

                string languageParameter =
                    commandLineArgs.FirstOfMatchingRegex(pattern: "^-lang=[a-zA-Z]{1,}$") ?? string.Empty;

                string[] splittedByEqualSign = languageParameter.Split('=');

                string language = splittedByEqualSign.LastOrDefault() ?? string.Empty;

                if (!LanguageCultureMappings.Mappings.ContainsKey(key: language))
                {
                    _console.WriteLine(text: $"Language \"{language}\" not supported.");
                    language = "en";
                }

                language = language.IsNullOrEmpty() ? "en" : language;

                return new LanguageParameter
                {
                    IsPresent = languageParameter.IsFilled(),
                    Language = language
                };
            });
        }
    }
}