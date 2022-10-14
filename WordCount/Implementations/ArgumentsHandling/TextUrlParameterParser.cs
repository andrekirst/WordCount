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
            return CachedValue(() =>
            {
                var args = Environment.GetCommandLineArgs();
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
}