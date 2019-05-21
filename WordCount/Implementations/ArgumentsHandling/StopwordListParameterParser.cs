using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class StopwordListParameterParser : BaseParameterParser<StopwordListParameter>, IStopwordListParameterParser
    {
        private IEnvironment Environment { get; }

        public StopwordListParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public StopwordListParameter ParseStopwordListParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] args = Environment.GetCommandLineArgs();

                string dictionaryParameter = args?.FirstOfMatchingRegex(pattern: @"-stopwordlist=[a-zA-z.]{1,}");
                string[] parameterSplitByEqualSign = dictionaryParameter?.Split('=');

                return new StopwordListParameter
                {
                    IsPresent = dictionaryParameter?.IsFilled() ?? false,
                    FileName = parameterSplitByEqualSign?.LastOrDefault()
                };
            });
        }
    }
}
