using System;
using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class TextUrlParameterParser : BaseParameterParser<TextUrlParameter>, ITextUrlParameterParser
    {
        private readonly IEnvironment _environment;

        public TextUrlParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public TextUrlParameter ParseTextUrlParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] args = _environment.GetCommandLineArgs();
                string texturlParameter =
                    args?.FirstOfMatchingRegex(pattern: @"-texturl=[a-zA-z.]{1,}") ?? string.Empty;
                string[] parameterSplitByEqualSign = texturlParameter?.Split('=') ?? Array.Empty<string>();

                bool isPresent =
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
}