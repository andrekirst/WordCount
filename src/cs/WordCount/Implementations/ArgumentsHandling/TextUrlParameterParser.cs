using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class TextUrlParameterParser(IEnvironment environment) : BaseParameterParser<TextUrlParameter>, ITextUrlParameterParser
{
    public TextUrlParameter ParseTextUrlParameter()
    {
        return CachedValue(() =>
        {
            var args = environment.GetCommandLineArgs();
            var texturlParameter =
                args?.FirstOfMatchingRegex(@"-texturl=[a-zA-z.]{1,}") ?? string.Empty;
            var parameterSplitByEqualSign = texturlParameter.Split('=');

            var isPresent =
                texturlParameter.IsFilled() &&
                parameterSplitByEqualSign.LastOrDefault().IsValidUrl();

            return new TextUrlParameter
            {
                IsPresent = isPresent,
                Url = isPresent ? parameterSplitByEqualSign.LastOrDefault() : null
            };
        });
    }
}