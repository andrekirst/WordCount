using System.Linq;
using WordCount.Abstractions.Console;
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

            // TODO Eventuell noch etwas lesbarer umbauen. Bsp.: als ExtensionMethod für IEnumerable
            string dictionaryArgumentValue = args.FirstOrDefault(predicate: p => p.IsMatchingRegex(pattern: @"-dictionary=[a-zA-z.]{1,}"));

            return new DictionaryParameter()
            {
                IsPresent = !dictionaryArgumentValue.IsNullOrEmpty()
            };
        }
    }
}
