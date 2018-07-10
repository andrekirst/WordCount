using System;
using System.Linq;
using WordCount.Extensions;
using WordCount.Abstractions.Environment;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class LanguageParameterParser : ILanguageParameterParser
    {
        private readonly IEnvironment _environment;

        public LanguageParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public LanguageParameter ParseLanguageParameter()
        {
            string[] commandLineArgs = _environment.GetCommandLineArgs() ?? Array.Empty<string>();

            string languageParameter = commandLineArgs.FirstOfMatchingRegex(pattern: "^-lang=(de|en)$") ?? string.Empty;

            string[] splittedByEqualSign = languageParameter.Split('=');

            string language = splittedByEqualSign.LastOrDefault();
            language = language.IsNullOrEmpty() ? "en" : language;

            return new LanguageParameter
            {
                IsPresent = languageParameter.IsFilled(),
                Language = language
            };
        }
    }
}