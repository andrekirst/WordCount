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
            return CachedValue(() =>
            {
                var args = Environment.GetCommandLineArgs();

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
}
