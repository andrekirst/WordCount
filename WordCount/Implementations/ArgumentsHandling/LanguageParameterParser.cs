using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class LanguageParameterParser : BaseParameterParser<LanguageParameter>, ILanguageParameterParser
    {
        private readonly IEnvironment _environment;

        public LanguageParameterParser(
            IEnvironment environment)
        {
            _environment = environment;
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

                return new LanguageParameter
                {
                    IsPresent = languageParameter.IsFilled(),
                    Language = language
                };
            });
        }
    }
}