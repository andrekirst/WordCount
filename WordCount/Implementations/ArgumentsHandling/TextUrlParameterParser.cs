using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class TextUrlParameterParser : BaseParameterParser<TextUrlParameter>, ITextUrlParameterParser
    {
        private IEnvironment Environment { get; }

        public TextUrlParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public TextUrlParameter ParseTextUrlParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] args = Environment.GetCommandLineArgs();
                string texturlParameter =
                    args?.FirstOfMatchingRegex(pattern: @"-texturl=[a-zA-z.]{1,}") ?? string.Empty;
                string[] parameterSplitByEqualSign = texturlParameter.Split('=');

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