using System.Linq;
using WordCount.Interfaces;
using WordCount.Models;

namespace WordCount.Implementations
{
    public class ConsoleParameterArgumentsReader : IArgumentsReader
    {
        public ArgumentsReaderResult ReadArguments(string[] args)
        {
            bool isSourceTextFilePresent = args != null && args.Any();

            return new ArgumentsReaderResult(
                sourceTextFile: isSourceTextFilePresent ? args[0] : null,
                isSourceTextFilePresent: isSourceTextFilePresent);
        }
    }
}
