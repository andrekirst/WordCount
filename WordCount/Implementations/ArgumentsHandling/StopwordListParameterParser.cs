using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class StopwordListParameterParser : IStopwordListParameterParser
    {
        private readonly IEnvironment _environment;

        public StopwordListParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public StopwordListParameter ParseStopwordListParameter()
        {
            string[] args = _environment.GetCommandLineArgs();

            string dictionaryParameter = args?.FirstOfMatchingRegex(pattern: @"-stopwordlist=[a-zA-z.]{1,}");
            string[] parameterSplitByEqualSign = dictionaryParameter?.Split('=');

            return new StopwordListParameter
            {
                IsPresent = dictionaryParameter?.IsFilled() ?? false,
                FileName = parameterSplitByEqualSign?.LastOrDefault()
            };
        }
    }
}
