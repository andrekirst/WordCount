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
        private readonly IEnvironment _environment;

        public SourceFileParameterParser(IEnvironment environment)
        {
            _environment = environment;
        }

        public SourceFileParameter ParseSourceFileParameter()
        {
            return CachedValue(toCachingValue: () =>
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
            });
        }
    }
}
