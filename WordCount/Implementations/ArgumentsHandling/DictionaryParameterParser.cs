using System.Linq;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class DictionaryParameterParser : IDictionaryParameterParser
    {
        public DictionaryParameter ParseDictionaryParameter(string[] args)
        {
            args = args ?? new string[0];

            string dictionaryArgumentValue =
                args.FirstOrDefault(predicate: p => p.IsMatchingRegex(pattern: @"-dictionary=[a-zA-z.]{1,}"));

            return new DictionaryParameter()
            {
                IsPresent = !dictionaryArgumentValue.IsNullOrEmpty()
            };
        }
    }
}
