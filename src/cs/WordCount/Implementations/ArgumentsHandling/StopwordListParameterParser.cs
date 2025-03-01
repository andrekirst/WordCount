using System.Linq;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling;

public class StopwordListParameterParser : BaseParameterParser<StopwordListParameter>, IParameterParser<StopwordListParameter>
{
    public StopwordListParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
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
