using System.Linq;
using WordCount.Extensions;
using WordCount.Models.Parameters;

namespace WordCount;

public class LanguageParameterParser : BaseParameterParser<LanguageParameter>, IParameterParser<LanguageParameter>
{
    public LanguageParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
            var languageParameter = args.FirstOfMatchingRegex("^-lang=[a-zA-Z]{1,}$") ?? string.Empty;
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