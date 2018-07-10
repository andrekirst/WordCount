using System;
using System.Linq;
using WordCount.Abstractions.Environment;
using WordCount.Extensions;
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

            string fileName = commandLineArgs
                                  .FirstOrDefault(predicate: s => !s.StartsWith(value: "-")) ?? string.Empty;

            bool isPresent = fileName.IsFilled();

            return new SourceFileParameter
            {
                IsPresent = isPresent,
                FileName = fileName
            };
        }
    }
}
