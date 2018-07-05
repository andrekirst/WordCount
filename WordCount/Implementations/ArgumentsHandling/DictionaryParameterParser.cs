using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class DictionaryParameterParser : IDictionaryParameterParser
    {
        private readonly IEnvironment _environment;

        public DictionaryParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public DictionaryParameter ParseDictionaryParameter()
        {
            string[] args = _environment.GetCommandLineArgs();

            string dictionaryParameter = args?.FirstOfMatchingRegex(pattern: @"-dictionary=[a-zA-z.]{1,}");
            string[] parameterSplitByEqualSign = dictionaryParameter?.Split('=');

            return new DictionaryParameter
            {
                IsPresent = dictionaryParameter?.IsFilled() ?? false,
                FileName = parameterSplitByEqualSign?.LastOrDefault()
            };
        }
    }
}
