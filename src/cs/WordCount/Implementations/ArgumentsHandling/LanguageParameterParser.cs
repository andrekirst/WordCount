using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class LanguageParameterParser(
    IEnvironment environment) : BaseParameterParser<LanguageParameter>, ILanguageParameterParser
{
    public LanguageParameter ParseLanguageParameter()
    {
        return CachedValue(() =>
        {
            var commandLineArgs = environment.GetCommandLineArgs() ?? Array.Empty<string>();

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