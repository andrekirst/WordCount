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
        private IEnvironment Environment { get; }

        public LanguageParameterParser(
            IEnvironment environment)
        {
            Environment = environment;
        }

        public LanguageParameter ParseLanguageParameter()
        {
            return CachedValue(() =>
            {
                var commandLineArgs = Environment.GetCommandLineArgs() ?? Array.Empty<string>();

                var languageParameter =
                    commandLineArgs.FirstOfMatchingRegex("^-lang=[a-zA-Z]{1,}$") ?? string.Empty;

                var splittedByEqualSign = languageParameter.Split('=');

                var language = splittedByEqualSign.LastOrDefault() ?? string.Empty;

                return new LanguageParameter
                {
                    IsPresent = languageParameter.IsFilled(),
                    Language = language
                };
            });
        }
    }
}