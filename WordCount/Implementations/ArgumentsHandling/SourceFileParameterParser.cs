using System.Linq;
using WordCount.Abstractions.Console;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class SourceFileParameterParser : ISourceFileParameterParser
    {
        private readonly IEnvironment _environment;

        public SourceFileParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public SourceFileParameter ParseSourceFileParameter()
        {
            string[] commandLineArgs = _environment.GetCommandLineArgs() ?? new string[0];

            bool isPresent = commandLineArgs.Any();
            string fileName = isPresent ? commandLineArgs[0] : null;

            return new SourceFileParameter()
            {
                IsPresent = isPresent,
                FileName = fileName
            };
        }
    }
}
