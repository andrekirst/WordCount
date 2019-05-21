using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class DictionaryParameterParser : BaseParameterParser<DictionaryParameter>, IDictionaryParameterParser
    {
        private IEnvironment Environment { get; }

        public DictionaryParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public DictionaryParameter ParseDictionaryParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] args = Environment.GetCommandLineArgs();

                string dictionaryParameter = args?.FirstOfMatchingRegex(pattern: @"-dictionary=[a-zA-z.]{1,}");
                string[] parameterSplitByEqualSign = dictionaryParameter?.Split('=');

                return new DictionaryParameter
                {
                    IsPresent = dictionaryParameter?.IsFilled() ?? false,
                    FileName = parameterSplitByEqualSign?.LastOrDefault()
                };
            });
        }
    }
}
