using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class TextUrlParameterParser : ITextUrlParameterParser
    {
        private readonly IEnvironment _environment;

        public TextUrlParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public TextUrlParameter ParseTextUrlParameter()
        {
            string[] args = _environment.GetCommandLineArgs();
            string texturlParameter = args?.FirstOfMatchingRegex(pattern: @"-texturl=[a-zA-z.]{1,}");
            string[] parameterSplitByEqualSign = texturlParameter?.Split('=');

            bool isPresent =
                texturlParameter.IsFilled() &&
                parameterSplitByEqualSign.LastOrDefault().IsValidUrl();

            return new TextUrlParameter
            {
                IsPresent = isPresent
            };
        }
    }
}