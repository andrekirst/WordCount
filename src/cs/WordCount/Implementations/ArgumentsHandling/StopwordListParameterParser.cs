using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class StopwordListParameterParser(IEnvironment environment) : BaseParameterParser<StopwordListParameter>, IStopwordListParameterParser
{
    public StopwordListParameter ParseStopwordListParameter()
    {
        return CachedValue(() =>
        {
            var args = environment.GetCommandLineArgs();

            var dictionaryParameter = args?.FirstOfMatchingRegex(@"-stopwordlist=[a-zA-z.]{1,}");
            var parameterSplitByEqualSign = dictionaryParameter?.Split('=');

            return new StopwordListParameter
            {
                IsPresent = dictionaryParameter?.IsFilled() ?? false,
                FileName = parameterSplitByEqualSign?.LastOrDefault()
            };
        });
    }
}
