using System;
using System.Linq;
using WordCount.Abstractions.SystemAbstractions;
using WordCount.Extensions;
using WordCount.Interfaces.ArgumentsHandling;
using WordCount.Models.Parameters;

namespace WordCount.Implementations.ArgumentsHandling
{
    public class SourceFileParameterParser : BaseParameterParser<SourceFileParameter>, ISourceFileParameterParser
    {
        private IEnvironment Environment { get; }

        public SourceFileParameterParser(IEnvironment environment)
        {
            Environment = environment;
        }

        public SourceFileParameter ParseSourceFileParameter()
        {
            return CachedValue(toCachingValue: () =>
            {
                string[] commandLineArgs = Environment.GetCommandLineArgs() ?? Array.Empty<string>();

                string fileName = commandLineArgs
                                      .FirstOrDefault(predicate: s => !s.StartsWith(value: "-")) ?? string.Empty;

                bool isPresent = fileName.IsFilled();

                return new SourceFileParameter
                {
                    IsPresent = isPresent,
                    FileName = fileName
                };
            });
        }
    }
}
