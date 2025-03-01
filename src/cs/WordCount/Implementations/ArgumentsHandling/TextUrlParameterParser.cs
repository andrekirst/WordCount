using System.Linq;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class TextUrlParameterParser : BaseParameterParser<TextUrlParameter>, IParameterParser<TextUrlParameter>
{
    public TextUrlParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
            var texturlParameter = args?.FirstOfMatchingRegex(@"-texturl=[a-zA-z.]{1,}") ?? string.Empty;
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