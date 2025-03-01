using System;
using System.Linq;
using WordCount.Extensions;
using WordCount.Models.Parameters;

namespace WordCount;

public class DictionaryParameterParser : BaseParameterParser<DictionaryParameter>, IParameterParser<DictionaryParameter>
{
    public DictionaryParameter ParseParameter(string[] args)
    {
        return CachedValue(() =>
        {
            var dictionaryParameter = args?.FirstOfMatchingRegex(@"-dictionary=[a-zA-z.]{1,}");
            var parameterSplitByEqualSign = dictionaryParameter?.Split('=');

            return new DictionaryParameter
            {
                IsPresent = dictionaryParameter?.IsFilled() ?? false,
                FileName = parameterSplitByEqualSign?.LastOrDefault()
            };
        });
    }
}
