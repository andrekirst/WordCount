using System;
using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

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
            string[] commandLineArgs = _environment.GetCommandLineArgs() ?? Array.Empty<string>();

            bool isPresent = commandLineArgs.Any(predicate: s => !s.StartsWith(value: "-"));
            string fileName = commandLineArgs.FirstOrDefault() ?? string.Empty;

            return new SourceFileParameter
            {
                IsPresent = isPresent,
                FileName = fileName
            };
        }
    }
}
